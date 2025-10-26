using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.Core.Infrastuctures.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Document>> GetDocumentsByLeaseIdAsync(int leaseId)
        {
            return await _dbSet
                .Where(d => d.LeaseId == leaseId)
                .ToListAsync();
        }

        public async Task<Document?> GetDocumentWithDetailsAsync(int documentId)
        {
            return await _dbSet
                .Include(d => d.Lease)
                .FirstOrDefaultAsync(d => d.Id == documentId);
        }
    }
}
