using HRAgents.Core.Interfaces;
using HRAgents.Infrastructure.Data;
using HRAgents.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HRAgents.Infrastructure;

/// <summary>
/// Extension methods for registering infrastructure services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers infrastructure services including repositories and session management.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register repositories as singletons to maintain in-memory data
        services.AddSingleton<IVacationRequestRepository, InMemoryVacationRequestRepository>();
        services.AddSingleton<ITimesheetEntryRepository, InMemoryTimesheetEntryRepository>();
        services.AddSingleton<IHRProcedureRepository, InMemoryHRProcedureRepository>();
        
        // Register session manager as singleton for session persistence
        services.AddSingleton<ISessionManager, InMemorySessionManager>();

        return services;
    }

    /// <summary>
    /// Seeds mock data for development.
    /// </summary>
    public static async Task SeedMockData(this IServiceProvider serviceProvider)
    {
        var vacationRepo = serviceProvider.GetRequiredService<IVacationRequestRepository>();
        var timesheetRepo = serviceProvider.GetRequiredService<ITimesheetEntryRepository>();
        var procedureRepo = serviceProvider.GetRequiredService<IHRProcedureRepository>();

        await MockDataSeeder.SeedAllData(vacationRepo, timesheetRepo, procedureRepo);
    }
}
