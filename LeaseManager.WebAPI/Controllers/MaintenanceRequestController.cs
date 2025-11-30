using LeaseManager.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static LeaseManager.WebAPI.Application.DTOs.MaintenanceRequestDTOs;

namespace LeaseManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceRequestController : ControllerBase
    {
        private readonly IMaintenanceRequestService _maintenanceRequestService;

        public MaintenanceRequestController(IMaintenanceRequestService maintenanceRequestService)
        {
            _maintenanceRequestService = maintenanceRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _maintenanceRequestService.GetAllAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = await _maintenanceRequestService.GetByIdAsync(id);
            if (request == null) return NotFound();
            return Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MaintenanceRequestCreateDto dto)
        {
            var request = await _maintenanceRequestService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MaintenanceRequestUpdateDto dto)
        {
            var success = await _maintenanceRequestService.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _maintenanceRequestService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}