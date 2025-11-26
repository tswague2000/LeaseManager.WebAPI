using Xunit;
using Moq;
using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;
using static LeaseManager.WebAPI.Application.DTOs.OwnerDTOs;

namespace ProjectTest.Services
{
    public class OwnerServiceUpdateAsyncTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IOwnerService _ownerService;

        public OwnerServiceUpdateAsyncTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _ownerService = new OwnerService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task UpdateAsync_WithValidData_ShouldUpdateOwner()
        {
            // Arrange
            var owner = TestDataBuilder.CreateTestOwner(1);
            var updateDto = new OwnerUpdateDto { FullName = "Updated Name" };

            _mockUnitOfWork.Setup(x => x.OwnerRepository.GetByIdAsync(1))
       .ReturnsAsync(owner);

            _mockUnitOfWork.Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

            // Act
            var result = await _ownerService.UpdateAsync(1, updateDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OwnerRepository.GetByIdAsync(999))
            .ReturnsAsync((Owner)null);

            // Act
            var result = await _ownerService.UpdateAsync(999, new OwnerUpdateDto());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallUpdate()
        {
            // Arrange
            var owner = TestDataBuilder.CreateTestOwner(1);
            var updateDto = new OwnerUpdateDto { Email = "newemail@example.com" };

            _mockUnitOfWork.Setup(x => x.OwnerRepository.GetByIdAsync(1))
           .ReturnsAsync(owner);

            _mockUnitOfWork.Setup(x => x.SaveChangesAsync())
          .ReturnsAsync(1);

            // Act
            await _ownerService.UpdateAsync(1, updateDto);

            // Assert
            _mockUnitOfWork.Verify(x => x.OwnerRepository.Update(It.IsAny<Owner>()), Times.Once);
        }
    }
}