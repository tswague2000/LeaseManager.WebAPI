using Xunit;
using Moq;
using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Application.Services;
using static LeaseManager.WebAPI.Application.DTOs.OwnerDTOs;

namespace ProjectTest.Services
{
    public class OwnerServiceCreateAsyncTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
       private readonly IOwnerService _ownerService;

    public OwnerServiceCreateAsyncTests()
        {
   _mockUnitOfWork = new Mock<IUnitOfWork>();
    _ownerService = new OwnerService(_mockUnitOfWork.Object);
 }

  [Fact]
        public async Task CreateAsync_WithValidData_ShouldCreateOwner()
     {
  // Arrange
 var createDto = new OwnerCreateDto
      {
FullName = "John Smith",
       Email = "john.smith@example.com",
        PhoneNumber = "555-0100"
     };

  _mockUnitOfWork.Setup(x => x.OwnerRepository.OwnerExistsByEmailAsync("john.smith@example.com"))
       .ReturnsAsync(false);

_mockUnitOfWork.Setup(x => x.OwnerRepository.AddAsync(It.IsAny<Owner>()))
        .Returns(Task.CompletedTask);

    _mockUnitOfWork.Setup(x => x.SaveChangesAsync())
 .ReturnsAsync(1);

    // Act
 var result = await _ownerService.CreateAsync(createDto);

      // Assert
 Assert.NotNull(result);
   Assert.Equal("John Smith", result.FullName);
     }

[Fact]
     public async Task CreateAsync_WithDuplicateEmail_ShouldThrowException()
        {
         // Arrange
  var createDto = new OwnerCreateDto
   {
FullName = "Jane Doe",
    Email = "existing@example.com",
    PhoneNumber = "555-0200"
    };

       _mockUnitOfWork.Setup(x => x.OwnerRepository.OwnerExistsByEmailAsync("existing@example.com"))
      .ReturnsAsync(true);

  // Act & Assert
  await Assert.ThrowsAsync<InvalidOperationException>(() => _ownerService.CreateAsync(createDto));
    }

     [Fact]
       public async Task CreateAsync_ShouldSaveChanges()
     {
      // Arrange
   var createDto = new OwnerCreateDto
    {
     FullName = "Bob Johnson",
        Email = "bob@example.com",
      PhoneNumber = "555-0300"
     };

      _mockUnitOfWork.Setup(x => x.OwnerRepository.OwnerExistsByEmailAsync("bob@example.com"))
      .ReturnsAsync(false);

    _mockUnitOfWork.Setup(x => x.OwnerRepository.AddAsync(It.IsAny<Owner>()))
     .Returns(Task.CompletedTask);

_mockUnitOfWork.Setup(x => x.SaveChangesAsync())
      .ReturnsAsync(1);

    // Act
  await _ownerService.CreateAsync(createDto);

    // Assert
      _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
     }
    }
}