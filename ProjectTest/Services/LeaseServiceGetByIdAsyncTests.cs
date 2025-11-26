using Xunit;
using Moq;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;
using static LeaseManager.WebAPI.Application.DTOs.LeaseDTOs;

namespace ProjectTest.Services
{
    public class LeaseServiceGetByIdAsyncTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ILeaseService _leaseService;

        public LeaseServiceGetByIdAsyncTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _leaseService = new LeaseService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnLease()
        {
            // Arrange
            var lease = TestDataBuilder.CreateTestLease(1);

            _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(1))
                  .ReturnsAsync(lease);

            // Act
            var result = await _leaseService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1000, result.MonthlyRent);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(999))
             .ReturnsAsync((global::LeaseManager.Core.Domain.Entities.Lease)null);

            // Act
            var result = await _leaseService.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldCallRepositoryWithCorrectId()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(5))
                 .ReturnsAsync(TestDataBuilder.CreateTestLease(5));

            // Act
            await _leaseService.GetByIdAsync(5);

            // Assert
            _mockUnitOfWork.Verify(x => x.LeaseRepository.GetByIdAsync(5), Times.Once);
        }
    }
}