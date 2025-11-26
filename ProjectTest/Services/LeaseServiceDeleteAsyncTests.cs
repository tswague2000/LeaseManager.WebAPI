using Xunit;
using Moq;
using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;

namespace ProjectTest.Services
{
    public class LeaseServiceDeleteAsyncTests
   {
   private readonly Mock<IUnitOfWork> _mockUnitOfWork;
private readonly ILeaseService _leaseService;

        public LeaseServiceDeleteAsyncTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
      _leaseService = new LeaseService(_mockUnitOfWork.Object);
   }

 [Fact]
        public async Task DeleteAsync_WithValidId_ShouldDeleteLease()
   {
    // Arrange
         var lease = TestDataBuilder.CreateTestLease(1);

    _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(1))
      .ReturnsAsync(lease);

  _mockUnitOfWork.Setup(x => x.LeaseRepository.Delete(It.IsAny<Lease>()))
     .Verifiable();

  _mockUnitOfWork.Setup(x => x.SaveChangesAsync())
       .ReturnsAsync(1);

       // Act
   var result = await _leaseService.DeleteAsync(1);

     // Assert
  Assert.True(result);
      _mockUnitOfWork.Verify(x => x.LeaseRepository.Delete(It.IsAny<Lease>()), Times.Once);
    }

  [Fact]
 public async Task DeleteAsync_WithInvalidId_ShouldReturnFalse()
        {
  // Arrange
 _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(999))
   .ReturnsAsync((Lease)null);

      // Act
      var result = await _leaseService.DeleteAsync(999);

        // Assert
  Assert.False(result);
        _mockUnitOfWork.Verify(x => x.LeaseRepository.Delete(It.IsAny<Lease>()), Times.Never);
 }

      [Fact]
 public async Task DeleteAsync_ShouldSaveChanges()
      {
 // Arrange
         var lease = TestDataBuilder.CreateTestLease(1);

       _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(1))
   .ReturnsAsync(lease);

      _mockUnitOfWork.Setup(x => x.SaveChangesAsync())
     .ReturnsAsync(1);

    // Act
  await _leaseService.DeleteAsync(1);

      // Assert
    _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
 }
    }
}