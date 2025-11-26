using Xunit;
using Moq;
using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;
using static LeaseManager.WebAPI.Application.DTOs.LeaseDTOs;

namespace ProjectTest.Services
{
    public class LeaseServiceCreateAsyncTests
   {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ILeaseService _leaseService;

        public LeaseServiceCreateAsyncTests()
        {
         _mockUnitOfWork = new Mock<IUnitOfWork>();
       _leaseService = new LeaseService(_mockUnitOfWork.Object);
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
   Assert.Equal("Test Tenant", result.TenantName);
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
 public async Task CreateAsync_WithInvalidTenant_ShouldThrowException()
   {
 // Arrange
   var property = TestDataBuilder.CreateTestProperty(1);

     var createDto = new LeaseCreateDto
 {
 StartDate = DateTime.Now,
      EndDate = DateTime.Now.AddMonths(12),
    MonthlyRent = 1000,
TenantId = 999,
     PropertyId = 1
   };

         _mockUnitOfWork.Setup(x => x.PropertyRepository.GetByIdAsync(1))
    .ReturnsAsync(property);

   _mockUnitOfWork.Setup(x => x.TenantRepository.GetByIdAsync(999))
     .ReturnsAsync((Tenant)null);

      // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _leaseService.CreateAsync(createDto));
 }

      [Fact]
  public async Task CreateAsync_ShouldSaveChanges()
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
        await _leaseService.CreateAsync(createDto);

     // Assert
   _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    }
}