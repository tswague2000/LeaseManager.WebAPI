using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Interfaces;
using LeaseManager.WebAPI.Application.Common.Interfaces;
using static LeaseManager.WebAPI.Application.DTOs.DocumentDTOs;

namespace LeaseManager.WebAPI.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;

 public DocumentService(IUnitOfWork unitOfWork)
  {
        _unitOfWork = unitOfWork;
 }

    public async Task<IEnumerable<DocumentReadDto>> GetAllAsync()
  {
    var documents = await _unitOfWork.DocumentRepository.GetAllAsync();
  return documents.Select(d => new DocumentReadDto
        {
Id = d.Id,
     FileName = d.FileName,
     FilePath = d.FilePath,
     UploadedAt = d.UploadedAt,
  LeaseId = d.LeaseId
    });
      }

   public async Task<DocumentReadDto?> GetByIdAsync(int id)
{
    var document = await _unitOfWork.DocumentRepository.GetDocumentWithDetailsAsync(id);
    if (document == null) return null;

  return new DocumentReadDto
            {
   Id = document.Id,
      FileName = document.FileName,
      FilePath = document.FilePath,
         UploadedAt = document.UploadedAt,
      LeaseId = document.LeaseId
       };
  }

        public async Task<DocumentReadDto> CreateAsync(DocumentCreateDto dto)
        {
            var lease = await _unitOfWork.LeaseRepository.GetByIdAsync(dto.LeaseId);
            if (lease == null)
           throw new InvalidOperationException($"Lease with ID {dto.LeaseId} not found");

       var document = new Document
      {
         FileName = dto.FileName,
  FilePath = dto.FilePath,
            LeaseId = dto.LeaseId,
          Lease = lease
    };

      await _unitOfWork.DocumentRepository.AddAsync(document);
    await _unitOfWork.SaveChangesAsync();

      return new DocumentReadDto
       {
                Id = document.Id,
       FileName = document.FileName,
      FilePath = document.FilePath,
             UploadedAt = document.UploadedAt,
   LeaseId = document.LeaseId
            };
        }

     public async Task<bool> DeleteAsync(int id)
      {
        var document = await _unitOfWork.DocumentRepository.GetByIdAsync(id);
      if (document == null) return false;

            _unitOfWork.DocumentRepository.Delete(document);
            await _unitOfWork.SaveChangesAsync();

  return true;
        }
    }
}