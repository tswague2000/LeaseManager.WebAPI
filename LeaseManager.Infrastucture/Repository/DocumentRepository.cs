using LeaseManager.Core.Infrastuctures.Data;
using LeaseManager.Infrastucture.Interfaces;

namespace LeaseManager.WebAPI.Application.Repository
{
    public class DocumentRepository(AppDbContext context) : GenericRepository<Document>(context), IDocumentRepository
    {
    }
}
