using HRAgents.Api.Models;
using HRAgents.Api.Services;
using HRAgents.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRAgents.Api.Endpoints;

/// <summary>
/// Extension methods for mapping chat-related endpoints.
/// </summary>
public static class ChatEndpoints
{
    public static void MapChatEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/chat")
            .WithTags("Chat")
            .WithOpenApi();

        // POST /api/chat/message - Send message to agent
        group.MapPost("/message", async (
            [FromBody] ChatMessageRequest request,
            [FromServices] IAgentRouter agentRouter,
            [FromServices] ISessionManager sessionManager,
            CancellationToken cancellationToken) =>
        {
            // Validate request
            if (!IsValidAgentType(request.AgentType))
            {
                return Results.BadRequest(ApiResponse<ChatMessageResponse>.Fail(
                    "Invalid agent type. Must be 'vacation', 'procedure', or 'timesheet'"));
            }

            // Get or create session
            var userId = request.UserId ?? "anonymous";
            sessionManager.GetOrCreateSession(request.SessionId, userId);

            // Route message to appropriate agent
            var response = await agentRouter.RouteMessageAsync(
                request.AgentType,
                request.Message,
                request.SessionId,
                cancellationToken);

            var chatResponse = new ChatMessageResponse
            {
                Response = response,
                SessionId = request.SessionId,
                AgentType = request.AgentType
            };

            return Results.Ok(ApiResponse<ChatMessageResponse>.Ok(chatResponse));
        })
        .WithName("SendChatMessage")
        .WithSummary("Send a message to a specific agent")
        .Produces<ApiResponse<ChatMessageResponse>>(StatusCodes.Status200OK)
        .Produces<ApiResponse<ChatMessageResponse>>(StatusCodes.Status400BadRequest);

        // GET /api/chat/history/{sessionId} - Get chat history
        group.MapGet("/history/{sessionId}", (
            string sessionId,
            [FromServices] ISessionManager sessionManager) =>
        {
            var session = sessionManager.GetSession(sessionId);

            if (session == null)
            {
                return Results.NotFound(ApiResponse<ChatHistoryResponse>.Fail(
                    "Session not found", 404));
            }

            var history = new ChatHistoryResponse
            {
                SessionId = session.SessionId,
                UserId = session.UserId,
                Messages = session.Messages.Select(m => new ChatHistoryMessage
                {
                    Role = m.Role,
                    Content = m.Content,
                    Timestamp = m.Timestamp,
                    AgentName = m.AgentName
                }).ToList(),
                CreatedAt = session.CreatedAt,
                LastActivityAt = session.LastActivityAt
            };

            return Results.Ok(ApiResponse<ChatHistoryResponse>.Ok(history));
        })
        .WithName("GetChatHistory")
        .WithSummary("Get the conversation history for a session")
        .Produces<ApiResponse<ChatHistoryResponse>>(StatusCodes.Status200OK)
        .Produces<ApiResponse<ChatHistoryResponse>>(StatusCodes.Status404NotFound);

        // POST /api/chat/stream - Stream message to agent (SSE)
        group.MapPost("/stream", async (
            [FromBody] ChatMessageRequest request,
            [FromServices] IAgentRouter agentRouter,
            [FromServices] ISessionManager sessionManager,
            HttpContext httpContext,
            CancellationToken cancellationToken) =>
        {
            // Validate request
            if (!IsValidAgentType(request.AgentType))
            {
                return Results.BadRequest(ApiResponse<string>.Fail(
                    "Invalid agent type. Must be 'vacation', 'procedure', or 'timesheet'"));
            }

            // Get or create session
            var userId = request.UserId ?? "anonymous";
            sessionManager.GetOrCreateSession(request.SessionId, userId);

            // Set up Server-Sent Events (SSE)
            httpContext.Response.Headers.Append("Content-Type", "text/event-stream");
            httpContext.Response.Headers.Append("Cache-Control", "no-cache");
            httpContext.Response.Headers.Append("Connection", "keep-alive");

            // Stream response
            await foreach (var chunk in agentRouter.RouteMessageStreamingAsync(
                request.AgentType,
                request.Message,
                request.SessionId,
                cancellationToken))
            {
                await httpContext.Response.WriteAsync($"data: {chunk}\n\n", cancellationToken);
                await httpContext.Response.Body.FlushAsync(cancellationToken);
            }

            await httpContext.Response.WriteAsync("data: [DONE]\n\n", cancellationToken);
            await httpContext.Response.Body.FlushAsync(cancellationToken);

            return Results.Ok();
        })
        .WithName("StreamChatMessage")
        .WithSummary("Stream a message response from an agent using Server-Sent Events")
        .Produces(StatusCodes.Status200OK)
        .Produces<ApiResponse<string>>(StatusCodes.Status400BadRequest);
    }

    private static bool IsValidAgentType(string agentType)
    {
        return agentType.ToLowerInvariant() is "vacation" or "procedure" or "timesheet";
    }
}
