using Xunit;
using Moq;
using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;
using static LeaseManager.WebAPI.Application.DTOs.LeaseDTOs;

namespace ProjectTest.Services
{
    public class LeaseServiceUpdateAsyncTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ILeaseService _leaseService;

        public LeaseServiceUpdateAsyncTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _leaseService = new LeaseService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task UpdateAsync_WithValidData_ShouldUpdateLease()
        {
            // Arrange
            var lease = TestDataBuilder.CreateTestLease(1);
            var updateDto = new LeaseUpdateDto { MonthlyRent = 1200 };

            _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(1))
              .ReturnsAsync(lease);

            _mockUnitOfWork.Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

            // Act
            var result = await _leaseService.UpdateAsync(1, updateDto);

            // Assert
            Assert.True(result);
            _mockUnitOfWork.Verify(x => x.LeaseRepository.Update(It.IsAny<Lease>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(999))
            .ReturnsAsync((Lease)null);

            // Act
            var result = await _leaseService.UpdateAsync(999, new LeaseUpdateDto());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_WithAllFields_ShouldUpdateAll()
        {
            // Arrange
            var lease = TestDataBuilder.CreateTestLease(1);
            var newStartDate = DateTime.Now.AddDays(1);
            var newEndDate = DateTime.Now.AddMonths(6);
            var updateDto = new LeaseUpdateDto
            {
                StartDate = newStartDate,
                EndDate = newEndDate,
                MonthlyRent = 1500
            };

            _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(1))
          .ReturnsAsync(lease);

            _mockUnitOfWork.Setup(x => x.SaveChangesAsync())
              .ReturnsAsync(1);

            // Act
            var result = await _leaseService.UpdateAsync(1, updateDto);

            // Assert
            Assert.True(result);
            Assert.Equal(1500, lease.MonthlyRent);
        }

        [Fact]
        public async Task UpdateAsync_WithPartialData_ShouldUpdateOnlyProvidedFields()
        {
            // Arrange
            var lease = TestDataBuilder.CreateTestLease(1);
            var originalStartDate = lease.StartDate;
            var updateDto = new LeaseUpdateDto { MonthlyRent = 1100 };

            _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(1))
          .ReturnsAsync(lease);

            _mockUnitOfWork.Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

            // Act
            var result = await _leaseService.UpdateAsync(1, updateDto);

            // Assert
            Assert.True(result);
            Assert.Equal(1100, lease.MonthlyRent);
            Assert.Equal(originalStartDate, lease.StartDate);
        }
    }
}