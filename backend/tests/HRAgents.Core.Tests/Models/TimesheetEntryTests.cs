using FluentAssertions;
using HRAgents.Core.Models;
using Xunit;

namespace HRAgents.Core.Tests.Models;

public class TimesheetEntryTests
{
    [Fact]
    public void TimesheetEntry_Should_CreateWithValidData()
    {
        // Arrange & Act
        var entry = new TimesheetEntry
        {
            Id = "ts-001",
            EmployeeId = "emp-001",
            Date = DateTime.Today,
            Hours = 8.0m,
            ProjectCode = "PROJ-001",
            Description = "Development work",
            Status = TimesheetStatus.Draft,
            CreatedAt = DateTime.UtcNow
        };

        // Assert
        entry.Should().NotBeNull();
        entry.Id.Should().Be("ts-001");
        entry.Hours.Should().Be(8.0m);
        entry.Status.Should().Be(TimesheetStatus.Draft);
    }

    [Fact]
    public void TimesheetEntry_Hours_Should_BePositive()
    {
        // Arrange
        var entry = new TimesheetEntry
        {
            Hours = -1
        };

        // Act & Assert
        entry.IsValid.Should().BeFalse();
    }

    [Fact]
    public void TimesheetEntry_Hours_Should_NotExceed24()
    {
        // Arrange
        var entry = new TimesheetEntry
        {
            Hours = 25
        };

        // Act & Assert
        entry.IsValid.Should().BeFalse();
    }

    [Fact]
    public void TimesheetEntry_Should_AllowSubmission()
    {
        // Arrange
        var entry = new TimesheetEntry
        {
            Status = TimesheetStatus.Draft,
            Hours = 8,
            Date = DateTime.Today,
            ProjectCode = "PROJ-001"
        };

        // Act
        entry.Submit();

        // Assert
        entry.Status.Should().Be(TimesheetStatus.Submitted);
    }

    [Fact]
    public void TimesheetEntry_Date_Should_NotBeFuture()
    {
        // Arrange
        var entry = new TimesheetEntry
        {
            Date = DateTime.Today.AddDays(1),
            Hours = 8,
            ProjectCode = "PROJ-001"
        };

        // Act & Assert
        entry.IsValid.Should().BeFalse();
    }
}
