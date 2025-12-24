using HRAgents.Api.Endpoints;
using HRAgents.Api.Middleware;
using HRAgents.Api.Services;
using HRAgents.Infrastructure;
using Microsoft.Extensions.AI;

var builder = WebApplication.CreateBuilder(args);

// Add CORS for local development
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentCors", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add infrastructure services (repositories and session management)
builder.Services.AddInfrastructure();

// Register mock chat client for development (replace with actual LLM in production)
builder.Services.AddHRAgents(sp => new MockChatClient());

// Register agent router
builder.Services.AddScoped<IAgentRouter, AgentRouter>();

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "HR Agents API", 
        Version = "v1",
        Description = "Multi-agent chat application for human resources services"
    });
});

// Add health checks
builder.Services.AddHealthChecks();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Seed mock data in development
if (app.Environment.IsDevelopment())
{
    await app.Services.SeedMockData();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR Agents API v1");
        c.RoutePrefix = string.Empty; // Serve Swagger at root
    });
    app.UseCors("DevelopmentCors");
}

// Add global error handling
app.UseErrorHandling();

app.UseHttpsRedirection();

// Map health check endpoint
app.MapHealthChecks("/health");

// Map API info endpoint
app.MapGet("/api/info", () => new
{
    Application = "HR Agents",
    Version = "1.0.0",
    Environment = app.Environment.EnvironmentName,
    AvailableAgents = new[] { "vacation", "procedure", "timesheet" }
})
.WithName("GetApiInfo")
.WithTags("Info")
.WithOpenApi();

// Map chat endpoints
app.MapChatEndpoints();

app.Run();
