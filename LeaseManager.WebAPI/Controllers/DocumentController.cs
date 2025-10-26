using LeaseManager.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static LeaseManager.WebAPI.Application.DTOs.DocumentDTOs;

namespace LeaseManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
{
        private readonly IDocumentService _documentService;

  public DocumentController(IDocumentService documentService)
     {
         _documentService = documentService;
     }

    [HttpGet]
      public async Task<IActionResult> GetAll()
        {
   var documents = await _documentService.GetAllAsync();
   return Ok(documents);
        }

        [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
        {
   var document = await _documentService.GetByIdAsync(id);
   if (document == null) return NotFound();
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentCreateDto dto)
        {
  var document = await _documentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = document.Id }, document);
        }

        [HttpDelete("{id}")]
     public async Task<IActionResult> Delete(int id)
        {
   var success = await _documentService.DeleteAsync(id);
            if (!success) return NotFound();
        return NoContent();
        }
    }
}