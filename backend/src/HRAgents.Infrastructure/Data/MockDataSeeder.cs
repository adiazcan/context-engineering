using HRAgents.Core.Interfaces;
using HRAgents.Core.Models;

namespace HRAgents.Infrastructure.Data;

/// <summary>
/// Seeds mock data for development purposes.
/// </summary>
public static class MockDataSeeder
{
    /// <summary>
    /// Seeds vacation request mock data.
    /// </summary>
    public static async Task SeedVacationRequests(IVacationRequestRepository repository)
    {
        var requests = new[]
        {
            new VacationRequest
            {
                Id = "vac-001",
                EmployeeId = "emp-001",
                StartDate = DateTime.Today.AddDays(7),
                EndDate = DateTime.Today.AddDays(11),
                Reason = "Family vacation to the beach",
                Status = VacationRequestStatus.Pending,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new VacationRequest
            {
                Id = "vac-002",
                EmployeeId = "emp-002",
                StartDate = DateTime.Today.AddDays(14),
                EndDate = DateTime.Today.AddDays(18),
                Reason = "Wedding anniversary",
                Status = VacationRequestStatus.Approved,
                ApprovedBy = "admin@hr.com",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-3)
            },
            new VacationRequest
            {
                Id = "vac-003",
                EmployeeId = "emp-001",
                StartDate = DateTime.Today.AddMonths(2),
                EndDate = DateTime.Today.AddMonths(2).AddDays(6),
                Reason = "Summer vacation",
                Status = VacationRequestStatus.Pending,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new VacationRequest
            {
                Id = "vac-004",
                EmployeeId = "emp-003",
                StartDate = DateTime.Today.AddDays(-10),
                EndDate = DateTime.Today.AddDays(-8),
                Reason = "Personal reasons",
                Status = VacationRequestStatus.Rejected,
                ApprovedBy = "admin@hr.com",
                CreatedAt = DateTime.UtcNow.AddDays(-12),
                UpdatedAt = DateTime.UtcNow.AddDays(-11)
            }
        };

        foreach (var request in requests)
        {
            await repository.AddAsync(request);
        }
    }

    /// <summary>
    /// Seeds timesheet entry mock data.
    /// </summary>
    public static async Task SeedTimesheetEntries(ITimesheetEntryRepository repository)
    {
        var entries = new[]
        {
            new TimesheetEntry
            {
                Id = "ts-001",
                EmployeeId = "emp-001",
                Date = DateTime.Today.AddDays(-1),
                Hours = 8.0m,
                ProjectCode = "PROJ-001",
                Description = "Backend API development",
                Status = TimesheetStatus.Draft,
                CreatedAt = DateTime.UtcNow
            },
            new TimesheetEntry
            {
                Id = "ts-002",
                EmployeeId = "emp-001",
                Date = DateTime.Today.AddDays(-2),
                Hours = 7.5m,
                ProjectCode = "PROJ-001",
                Description = "Code review and testing",
                Status = TimesheetStatus.Submitted,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new TimesheetEntry
            {
                Id = "ts-003",
                EmployeeId = "emp-002",
                Date = DateTime.Today.AddDays(-1),
                Hours = 8.0m,
                ProjectCode = "PROJ-002",
                Description = "Frontend development",
                Status = TimesheetStatus.Approved,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new TimesheetEntry
            {
                Id = "ts-004",
                EmployeeId = "emp-001",
                Date = DateTime.Today.AddDays(-3),
                Hours = 6.5m,
                ProjectCode = "PROJ-003",
                Description = "Bug fixes and maintenance",
                Status = TimesheetStatus.Submitted,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new TimesheetEntry
            {
                Id = "ts-005",
                EmployeeId = "emp-003",
                Date = DateTime.Today.AddDays(-1),
                Hours = 8.0m,
                ProjectCode = "PROJ-001",
                Description = "Database optimization",
                Status = TimesheetStatus.Draft,
                CreatedAt = DateTime.UtcNow
            }
        };

        foreach (var entry in entries)
        {
            await repository.AddAsync(entry);
        }
    }

    /// <summary>
    /// Seeds HR procedure mock data.
    /// </summary>
    public static async Task SeedHRProcedures(IHRProcedureRepository repository)
    {
        var procedures = new[]
        {
            new HRProcedure
            {
                Id = "proc-001",
                Title = "How to Request Vacation Time",
                Category = "Vacation",
                Steps = new List<ProcedureStep>
                {
                    new() { StepNumber = 1, Description = "Log in to the HR portal" },
                    new() { StepNumber = 2, Description = "Navigate to the Vacation Request section" },
                    new() { StepNumber = 3, Description = "Select your desired vacation dates" },
                    new() { StepNumber = 4, Description = "Provide a brief reason for your time off" },
                    new() { StepNumber = 5, Description = "Submit the request for manager approval" },
                    new() { StepNumber = 6, Description = "Wait for email confirmation of approval or denial" }
                },
                RelatedPolicies = "Employee Vacation Policy 2025"
            },
            new HRProcedure
            {
                Id = "proc-002",
                Title = "Submitting Weekly Timesheets",
                Category = "Timesheet",
                Steps = new List<ProcedureStep>
                {
                    new() { StepNumber = 1, Description = "Access the timesheet system before the weekly deadline" },
                    new() { StepNumber = 2, Description = "Enter hours worked for each day of the week" },
                    new() { StepNumber = 3, Description = "Assign hours to the correct project codes" },
                    new() { StepNumber = 4, Description = "Add descriptions for all entries" },
                    new() { StepNumber = 5, Description = "Review all entries for accuracy" },
                    new() { StepNumber = 6, Description = "Submit the timesheet for approval" }
                },
                RelatedPolicies = "Time Tracking and Reporting Policy"
            },
            new HRProcedure
            {
                Id = "proc-003",
                Title = "Requesting a Sick Day",
                Category = "Vacation",
                Steps = new List<ProcedureStep>
                {
                    new() { StepNumber = 1, Description = "Notify your manager as soon as possible" },
                    new() { StepNumber = 2, Description = "Log the sick day in the HR system" },
                    new() { StepNumber = 3, Description = "Provide a doctor's note if absent for more than 3 days" }
                },
                RelatedPolicies = "Sick Leave Policy 2025"
            },
            new HRProcedure
            {
                Id = "proc-004",
                Title = "Correcting Timesheet Errors",
                Category = "Timesheet",
                Steps = new List<ProcedureStep>
                {
                    new() { StepNumber = 1, Description = "Contact your manager to explain the error" },
                    new() { StepNumber = 2, Description = "Access the timesheet correction form" },
                    new() { StepNumber = 3, Description = "Provide details of the original entry and correction needed" },
                    new() { StepNumber = 4, Description = "Submit for manager and HR approval" }
                },
                RelatedPolicies = "Time Tracking and Reporting Policy"
            },
            new HRProcedure
            {
                Id = "proc-005",
                Title = "Annual Performance Review Process",
                Category = "Performance",
                Steps = new List<ProcedureStep>
                {
                    new() { StepNumber = 1, Description = "Complete your self-assessment form" },
                    new() { StepNumber = 2, Description = "Schedule a meeting with your manager" },
                    new() { StepNumber = 3, Description = "Discuss achievements and areas for improvement" },
                    new() { StepNumber = 4, Description = "Set goals for the upcoming year" },
                    new() { StepNumber = 5, Description = "Sign the performance review document" }
                },
                RelatedPolicies = "Performance Management Policy"
            }
        };

        foreach (var procedure in procedures)
        {
            await repository.AddAsync(procedure);
        }
    }

    /// <summary>
    /// Seeds all mock data.
    /// </summary>
    public static async Task SeedAllData(
        IVacationRequestRepository vacationRepository,
        ITimesheetEntryRepository timesheetRepository,
        IHRProcedureRepository procedureRepository)
    {
        await SeedVacationRequests(vacationRepository);
        await SeedTimesheetEntries(timesheetRepository);
        await SeedHRProcedures(procedureRepository);
    }
}
