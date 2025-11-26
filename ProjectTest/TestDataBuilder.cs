using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Enums;

namespace ProjectTest
{
    public static class TestDataBuilder
    {
        public static Property CreateTestProperty(int id = 1)
        {
            return new Property
            {
                Id = id,
                Title = "Test Apartment",
                Address = "123 Test St",
                City = "Test City",
                Province = "TC",
                PostalCode = "T1T1T1",
                RentPrice = 1000,
                Bedrooms = 2,
                Bathrooms = 1,
                IsAvailable = true,
                OwnerId = 1,
                Owner = CreateTestOwner(1),
                Images = new List<PropertyImage>(),
                Leases = new List<Lease>()
            };
        }

        public static Owner CreateTestOwner(int id = 1)
        {
            return new Owner
            {
                Id = id,
                FullName = "Test Owner",
                Email = $"owner{id}@example.com",
                PhoneNumber = "123456789",
                Properties = new List<Property>()
            };
        }

        public static Tenant CreateTestTenant(int id = 1)
        {
            return new Tenant
            {
                Id = id,
                FullName = "Test Tenant",
                Email = $"tenant{id}@example.com",
                PhoneNumber = "987654321",
                Leases = new List<Lease>()
            };
        }

        public static Lease CreateTestLease(int id = 1, int propertyId = 1, int tenantId = 1)
        {
            return new Lease
            {
                Id = id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(12),
                MonthlyRent = 1000,
                Status = LeaseStatus.Active,
                PropertyId = propertyId,
                TenantId = tenantId,
                Property = CreateTestProperty(propertyId),
                Tenant = CreateTestTenant(tenantId),
                Payments = new List<Payment>(),
                Documents = new List<Document>()
            };
        }

        public static Payment CreateTestPayment(int id = 1, int leaseId = 1)
        {
            return new Payment
            {
                Id = id,
                LeaseId = leaseId,
                Amount = 1000,
                PaymentDate = DateTime.Now,
                Status = PaymentStatus.Completed,
                Method = PaymentMethod.BankTransfer,
                Lease = CreateTestLease(leaseId)
            };
        }

        public static MaintenanceRequest CreateTestMaintenanceRequest(int id = 1, int propertyId = 1, int tenantId = 1)
        {
            return new MaintenanceRequest
            {
                Id = id,
                Description = "Test Maintenance",
                RequestDate = DateTime.Now,
                Status = MaintenanceStatus.Pending,
                PropertyId = propertyId,
                Property = CreateTestProperty(propertyId),
                TenantId = tenantId,
                Tenant = CreateTestTenant(tenantId)
            };
        }

        public static PropertyImage CreateTestPropertyImage(int id = 1, int propertyId = 1)
        {
            return new PropertyImage
            {
                Id = id,
                ImageUrl = $"http://example.com/image{id}.jpg",
                Description = "Test Image",
                PropertyId = propertyId,
                Property = CreateTestProperty(propertyId)
            };
        }

        public static Document CreateTestDocument(int id = 1, int leaseId = 1)
        {
            return new Document
            {
                Id = id,
                FileName = $"document{id}.pdf",
                FilePath = $"/documents/document{id}.pdf",
                UploadedAt = DateTime.Now,
                LeaseId = leaseId,
                Lease = CreateTestLease(leaseId)
            };
        }
    }
}