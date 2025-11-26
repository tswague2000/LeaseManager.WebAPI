using Xunit;
using Moq;
using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;
using static LeaseManager.WebAPI.Application.DTOs.OwnerDTOs;

namespace ProjectTest.Services
{
    public class OwnerServiceGetAllAsyncTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IOwnerService _ownerService;

        public OwnerServiceGetAllAsyncTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _ownerService = new OwnerService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllAsync_WithMultipleOwners_ShouldReturnAllOwners()
        {
            // Arrange
            var owners = new List<Owner>
 {
   TestDataBuilder.CreateTestOwner(1),
     TestDataBuilder.CreateTestOwner(2),
     TestDataBuilder.CreateTestOwner(3)
 };

            _mockUnitOfWork.Setup(x => x.OwnerRepository.GetAllAsync())
              .ReturnsAsync(owners);

            // Act
            var result = await _ownerService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_WithEmptyList_ShouldReturnEmptyCollection()
        {
            // Arrange
            var owners = new List<Owner>();

            _mockUnitOfWork.Setup(x => x.OwnerRepository.GetAllAsync())
        .ReturnsAsync(owners);

            // Act
            var result = await _ownerService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldCallRepositoryOnce()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.OwnerRepository.GetAllAsync())
                .ReturnsAsync(new List<Owner>());

            // Act
            await _ownerService.GetAllAsync();

            // Assert
            _mockUnitOfWork.Verify(x => x.OwnerRepository.GetAllAsync(), Times.Once);
        }
    }
}