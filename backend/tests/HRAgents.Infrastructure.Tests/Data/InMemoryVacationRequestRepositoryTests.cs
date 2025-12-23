using FluentAssertions;
using HRAgents.Core.Interfaces;
using HRAgents.Core.Models;
using HRAgents.Infrastructure.Data;
using Xunit;

namespace HRAgents.Infrastructure.Tests.Data;

public class InMemoryVacationRequestRepositoryTests
{
    private readonly IVacationRequestRepository _repository;

    public InMemoryVacationRequestRepositoryTests()
    {
        _repository = new InMemoryVacationRequestRepository();
    }

    [Fact]
    public async Task AddAsync_Should_AddVacationRequest()
    {
        // Arrange
        var request = new VacationRequest
        {
            Id = "vac-001",
            EmployeeId = "emp-001",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(5),
            Reason = "Vacation",
            Status = VacationRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = await _repository.AddAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("vac-001");
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnVacationRequest()
    {
        // Arrange
        var request = new VacationRequest
        {
            Id = "vac-002",
            EmployeeId = "emp-001",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(5),
            Status = VacationRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        await _repository.AddAsync(request);

        // Act
        var result = await _repository.GetByIdAsync("vac-002");

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be("vac-002");
    }

    [Fact]
    public async Task GetByEmployeeIdAsync_Should_ReturnEmployeeRequests()
    {
        // Arrange
        await _repository.AddAsync(new VacationRequest
        {
            Id = "vac-003",
            EmployeeId = "emp-001",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(5),
            Status = VacationRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        });
        await _repository.AddAsync(new VacationRequest
        {
            Id = "vac-004",
            EmployeeId = "emp-001",
            StartDate = DateTime.Today.AddDays(10),
            EndDate = DateTime.Today.AddDays(15),
            Status = VacationRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        });
        await _repository.AddAsync(new VacationRequest
        {
            Id = "vac-005",
            EmployeeId = "emp-002",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(3),
            Status = VacationRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        });

        // Act
        var results = await _repository.GetByEmployeeIdAsync("emp-001");

        // Assert
        results.Should().HaveCount(2);
        results.Should().OnlyContain(r => r.EmployeeId == "emp-001");
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateVacationRequest()
    {
        // Arrange
        var request = new VacationRequest
        {
            Id = "vac-006",
            EmployeeId = "emp-001",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(5),
            Status = VacationRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        await _repository.AddAsync(request);

        // Act
        request.Status = VacationRequestStatus.Approved;
        var updated = await _repository.UpdateAsync(request);

        // Assert
        updated.Status.Should().Be(VacationRequestStatus.Approved);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveVacationRequest()
    {
        // Arrange
        var request = new VacationRequest
        {
            Id = "vac-007",
            EmployeeId = "emp-001",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(5),
            Status = VacationRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        await _repository.AddAsync(request);

        // Act
        var deleted = await _repository.DeleteAsync("vac-007");

        // Assert
        deleted.Should().BeTrue();
        var result = await _repository.GetByIdAsync("vac-007");
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByStatusAsync_Should_ReturnRequestsWithStatus()
    {
        // Arrange
        await _repository.AddAsync(new VacationRequest
        {
            Id = "vac-008",
            EmployeeId = "emp-001",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(5),
            Status = VacationRequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        });
        await _repository.AddAsync(new VacationRequest
        {
            Id = "vac-009",
            EmployeeId = "emp-002",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(3),
            Status = VacationRequestStatus.Approved,
            CreatedAt = DateTime.UtcNow
        });

        // Act
        var results = await _repository.GetByStatusAsync(VacationRequestStatus.Pending);

        // Assert
        results.Should().Contain(r => r.Id == "vac-008");
        results.Should().NotContain(r => r.Id == "vac-009");
    }

    [Fact]
    public async Task Repository_Should_BeThreadSafe()
    {
        // Arrange & Act - Add 100 requests concurrently
        var tasks = Enumerable.Range(1, 100).Select(i =>
            _repository.AddAsync(new VacationRequest
            {
                Id = $"vac-{i}",
                EmployeeId = $"emp-{i % 10}",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(5),
                Status = VacationRequestStatus.Pending,
                CreatedAt = DateTime.UtcNow
            })
        );

        await Task.WhenAll(tasks);

        // Assert
        var all = await _repository.GetAllAsync();
        all.Should().HaveCount(100);
    }
}
