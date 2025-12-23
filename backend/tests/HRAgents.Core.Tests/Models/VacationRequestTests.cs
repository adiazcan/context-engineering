using FluentAssertions;
using HRAgents.Core.Models;
using Xunit;

namespace HRAgents.Core.Tests.Models;

public class VacationRequestTests
{
    [Fact]
    public void VacationRequest_Should_CreateWithValidData()
    {
        // Arrange & Act
        var request = new VacationRequest
        {
            Id = "vac-001",
            EmployeeId = "emp-001",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(5),
            Reason = "Family vacation",
            Status = VacationRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        // Assert
        request.Should().NotBeNull();
        request.Id.Should().Be("vac-001");
        request.EmployeeId.Should().Be("emp-001");
        request.Status.Should().Be(VacationRequestStatus.Pending);
    }

    [Fact]
    public void VacationRequest_Should_CalculateDaysCorrectly()
    {
        // Arrange
        var request = new VacationRequest
        {
            StartDate = new DateTime(2025, 12, 23),
            EndDate = new DateTime(2025, 12, 27)
        };

        // Act
        var days = request.TotalDays;

        // Assert
        days.Should().Be(5); // Including both start and end date
    }

    [Fact]
    public void VacationRequest_EndDate_Should_BeAfterStartDate()
    {
        // Arrange
        var request = new VacationRequest
        {
            StartDate = DateTime.Today.AddDays(5),
            EndDate = DateTime.Today
        };

        // Act & Assert
        request.IsValid.Should().BeFalse();
    }

    [Fact]
    public void VacationRequest_Should_AllowApproval()
    {
        // Arrange
        var request = new VacationRequest
        {
            Status = VacationRequestStatus.Pending
        };

        // Act
        request.Approve("manager-001");

        // Assert
        request.Status.Should().Be(VacationRequestStatus.Approved);
        request.ApprovedBy.Should().Be("manager-001");
        request.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void VacationRequest_Should_AllowRejection()
    {
        // Arrange
        var request = new VacationRequest
        {
            Status = VacationRequestStatus.Pending
        };

        // Act
        request.Reject("manager-001");

        // Assert
        request.Status.Should().Be(VacationRequestStatus.Rejected);
        request.ApprovedBy.Should().Be("manager-001");
        request.UpdatedAt.Should().NotBeNull();
    }
}
