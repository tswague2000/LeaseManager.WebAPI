using Xunit;
using Moq;
using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;

namespace ProjectTest.Services
{
    public class OwnerServiceDeleteAsyncTests
  {
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IOwnerService _ownerService;

  public OwnerServiceDeleteAsyncTests()
  {
  _mockUnitOfWork = new Mock<IUnitOfWork>();
         _ownerService = new OwnerService(_mockUnitOfWork.Object);
    }

   [Fact]
        public async Task DeleteAsync_WithValidId_ShouldDeleteOwner()
  {
      // Arrange
        var owner = TestDataBuilder.CreateTestOwner(1);

 _mockUnitOfWork.Setup(x => x.OwnerRepository.GetByIdAsync(1))
   .ReturnsAsync(owner);

       _mockUnitOfWork.Setup(x => x.OwnerRepository.Delete(It.IsAny<Owner>()))
      .Verifiable();

   _mockUnitOfWork.Setup(x => x.SaveChangesAsync())
        .ReturnsAsync(1);

        // Act
   var result = await _ownerService.DeleteAsync(1);

    // Assert
 Assert.True(result);
     _mockUnitOfWork.Verify(x => x.OwnerRepository.Delete(It.IsAny<Owner>()), Times.Once);
     }

  [Fact]
      public async Task DeleteAsync_WithInvalidId_ShouldReturnFalse()
  {
     // Arrange
 _mockUnitOfWork.Setup(x => x.OwnerRepository.GetByIdAsync(999))
      .ReturnsAsync((Owner)null);

       // Act
 var result = await _ownerService.DeleteAsync(999);

    // Assert
       Assert.False(result);
     _mockUnitOfWork.Verify(x => x.OwnerRepository.Delete(It.IsAny<Owner>()), Times.Never);
  }
 }
}