using LeaseManager.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static LeaseManager.WebAPI.Application.DTOs.TenantDTOs;

namespace LeaseManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tenants = await _tenantService.GetAllAsync();
            return Ok(tenants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tenant = await _tenantService.GetByIdAsync(id);
            if (tenant == null) return NotFound();
            return Ok(tenant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TenantCreateDto dto)
        {
            var tenant = await _tenantService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = tenant.Id }, tenant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TenantUpdateDto dto)
        {
            var success = await _tenantService.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _tenantService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}