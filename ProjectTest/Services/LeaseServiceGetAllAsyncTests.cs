using Xunit;
using Moq;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;
using static LeaseManager.WebAPI.Application.DTOs.LeaseDTOs;

namespace ProjectTest.Services
{
    public class LeaseServiceGetAllAsyncTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
     private readonly ILeaseService _leaseService;

        public LeaseServiceGetAllAsyncTests()
{
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _leaseService = new LeaseService(_mockUnitOfWork.Object);
      }

    [Fact]
        public async Task GetAllAsync_WithMultipleLeases_ShouldReturnAllLeases()
  {
            // Arrange
            var leases = new List<global::LeaseManager.Core.Domain.Entities.Lease>
          {
            TestDataBuilder.CreateTestLease(1),
       TestDataBuilder.CreateTestLease(2, 2, 2)
      };

    _mockUnitOfWork.Setup(x => x.LeaseRepository.GetAllAsync())
           .ReturnsAsync(leases);

    // Act
            var result = await _leaseService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
         Assert.Equal(2, result.Count());
      }

   [Fact]
        public async Task GetAllAsync_WithEmptyList_ShouldReturnEmptyCollection()
     {
            // Arrange
  var leases = new List<global::LeaseManager.Core.Domain.Entities.Lease>();

  _mockUnitOfWork.Setup(x => x.LeaseRepository.GetAllAsync())
          .ReturnsAsync(leases);

         // Act
         var result = await _leaseService.GetAllAsync();

         // Assert
      Assert.NotNull(result);
            Assert.Empty(result);
  }

        [Fact]
        public async Task GetAllAsync_ShouldCallRepositoryGetAllAsyncOnce()
        {
      // Arrange
       _mockUnitOfWork.Setup(x => x.LeaseRepository.GetAllAsync())
      .ReturnsAsync(new List<global::LeaseManager.Core.Domain.Entities.Lease>());

            // Act
            await _leaseService.GetAllAsync();

  // Assert
     _mockUnitOfWork.Verify(x => x.LeaseRepository.GetAllAsync(), Times.Once);
        }
    }
}