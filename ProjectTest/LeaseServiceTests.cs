using Xunit;
using Moq;
using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;
using static LeaseManager.WebAPI.Application.DTOs.LeaseDTOs;

namespace ProjectTest
{
    public class LeaseServiceTests
    {
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ILeaseService _leaseService;

        public LeaseServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
          _leaseService = new LeaseService(_mockUnitOfWork.Object);
   }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllLeases()
        {
            // Arrange
            var leases = new List<Lease>
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
        }

  [Fact]
        public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
     {
    // Arrange
            _mockUnitOfWork.Setup(x => x.LeaseRepository.GetByIdAsync(999))
    .ReturnsAsync((Lease)null);

       // Act
         var result = await _leaseService.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_WithValidData_ShouldCreateLease()
  {
            // Arrange
        var property = TestDataBuilder.CreateTestProperty(1);
   var tenant = TestDataBuilder.CreateTestTenant(1);

            var createDto = new LeaseCreateDto
  {
      StartDate = DateTime.Now,
      EndDate = DateTime.Now.AddMonths(12),
         MonthlyRent = 1000,
        TenantId = 1,
                PropertyId = 1
            };

  _mockUnitOfWork.Setup(x => x.PropertyRepository.GetByIdAsync(1))
   .ReturnsAsync(property);

            _mockUnitOfWork.Setup(x => x.TenantRepository.GetByIdAsync(1))
      .ReturnsAsync(tenant);

         _mockUnitOfWork.Setup(x => x.LeaseRepository.AddAsync(It.IsAny<Lease>()))
           .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

       // Act
            var result = await _leaseService.CreateAsync(createDto);

  // Assert
  Assert.NotNull(result);
            Assert.Equal(1000, result.MonthlyRent);
        }

        [Fact]
        public async Task CreateAsync_WithInvalidProperty_ShouldThrowException()
    {
         // Arrange
     var createDto = new LeaseCreateDto
            {
      StartDate = DateTime.Now,
   EndDate = DateTime.Now.AddMonths(12),
        MonthlyRent = 1000,
        TenantId = 1,
PropertyId = 999
            };

            _mockUnitOfWork.Setup(x => x.PropertyRepository.GetByIdAsync(999))
       .ReturnsAsync((Property)null);

       // Act & Assert
     await Assert.ThrowsAsync<InvalidOperationException>(() => _leaseService.CreateAsync(createDto));
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
  }
    }
}