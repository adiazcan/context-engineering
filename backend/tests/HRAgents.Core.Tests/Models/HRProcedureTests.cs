using FluentAssertions;
using HRAgents.Core.Models;
using Xunit;

namespace HRAgents.Core.Tests.Models;

public class HRProcedureTests
{
    [Fact]
    public void HRProcedure_Should_CreateWithValidData()
    {
        // Arrange & Act
        var procedure = new HRProcedure
        {
            Id = "proc-001",
            Title = "How to Request Vacation",
            Category = "Vacation",
            Steps = new List<ProcedureStep>
            {
                new() { StepNumber = 1, Description = "Step 1" },
                new() { StepNumber = 2, Description = "Step 2" }
            },
            RelatedPolicies = "Vacation Policy 2025"
        };

        // Assert
        procedure.Should().NotBeNull();
        procedure.Id.Should().Be("proc-001");
        procedure.Steps.Should().HaveCount(2);
    }

    [Fact]
    public void HRProcedure_Should_RequireTitle()
    {
        // Arrange
        var procedure = new HRProcedure
        {
            Title = "",
            Category = "Test"
        };

        // Act & Assert
        procedure.IsValid.Should().BeFalse();
    }

    [Fact]
    public void HRProcedure_Should_RequireCategory()
    {
        // Arrange
        var procedure = new HRProcedure
        {
            Title = "Test Procedure",
            Category = ""
        };

        // Act & Assert
        procedure.IsValid.Should().BeFalse();
    }

    [Fact]
    public void HRProcedure_Should_RequireAtLeastOneStep()
    {
        // Arrange
        var procedure = new HRProcedure
        {
            Title = "Test Procedure",
            Category = "Test",
            Steps = new List<ProcedureStep>()
        };

        // Act & Assert
        procedure.IsValid.Should().BeFalse();
    }
}

public class ProcedureStepTests
{
    [Fact]
    public void ProcedureStep_Should_CreateWithValidData()
    {
        // Arrange & Act
        var step = new ProcedureStep
        {
            StepNumber = 1,
            Description = "Complete form",
            Notes = "Use the online portal"
        };

        // Assert
        step.Should().NotBeNull();
        step.StepNumber.Should().Be(1);
        step.Description.Should().Be("Complete form");
    }

    [Fact]
    public void ProcedureStep_Should_RequireDescription()
    {
        // Arrange
        var step = new ProcedureStep
        {
            StepNumber = 1,
            Description = ""
        };

        // Act & Assert
        step.IsValid.Should().BeFalse();
    }
}
