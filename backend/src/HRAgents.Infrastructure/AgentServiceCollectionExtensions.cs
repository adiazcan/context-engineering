using HRAgents.Core.Interfaces;
using HRAgents.Agents.Vacation;
using HRAgents.Agents.Procedure;
using HRAgents.Agents.Timesheet;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.AI;

namespace HRAgents.Infrastructure;

/// <summary>
/// Extension methods for registering agents.
/// </summary>
public static class AgentServiceCollectionExtensions
{
    /// <summary>
    /// Registers all HR agents with their dependencies.
    /// Note: Requires an IChatClient to be registered in the service collection.
    /// </summary>
    public static IServiceCollection AddHRAgents(
        this IServiceCollection services,
        Func<IServiceProvider, IChatClient> chatClientFactory)
    {
        // Register the chat client factory
        services.AddSingleton(chatClientFactory);
        
        // Register agents as scoped services for per-request lifecycle
        services.AddScoped<IVacationAgent>(sp =>
        {
            var chatClient = chatClientFactory(sp);
            var sessionManager = sp.GetRequiredService<ISessionManager>();
            return new VacationAgent(chatClient, sessionManager);
        });
        
        services.AddScoped<IProcedureAgent>(sp =>
        {
            var chatClient = chatClientFactory(sp);
            var sessionManager = sp.GetRequiredService<ISessionManager>();
            return new ProcedureAgent(chatClient, sessionManager);
        });
        
        services.AddScoped<ITimesheetAgent>(sp =>
        {
            var chatClient = chatClientFactory(sp);
            var sessionManager = sp.GetRequiredService<ISessionManager>();
            return new TimesheetAgent(chatClient, sessionManager);
        });

        return services;
    }
}
