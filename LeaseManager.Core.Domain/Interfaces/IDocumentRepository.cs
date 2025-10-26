using LeaseManager.Core.Domain.Entities;

namespace LeaseManager.Core.Domain.Interfaces
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        Task<IEnumerable<Document>> GetDocumentsByLeaseIdAsync(int leaseId);
        Task<Document?> GetDocumentWithDetailsAsync(int documentId);
    }
}