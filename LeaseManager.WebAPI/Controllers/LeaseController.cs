using LeaseManager.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static LeaseManager.WebAPI.Application.DTOs.LeaseDTOs;

namespace LeaseManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaseController : ControllerBase
    {
 private readonly ILeaseService _leaseService;

        public LeaseController(ILeaseService leaseService)
        {
            _leaseService = leaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
 var leases = await _leaseService.GetAllAsync();
    return Ok(leases);
        }

[HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
        {
   var lease = await _leaseService.GetByIdAsync(id);
   if (lease == null) return NotFound();
 return Ok(lease);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeaseCreateDto dto)
 {
     var lease = await _leaseService.CreateAsync(dto);
       return CreatedAtAction(nameof(GetById), new { id = lease.Id }, lease);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LeaseUpdateDto dto)
        {
          var success = await _leaseService.UpdateAsync(id, dto);
            if (!success) return NotFound();
  return NoContent();
        }

        [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(int id)
        {
            var success = await _leaseService.DeleteAsync(id);
            if (!success) return NotFound();
       return NoContent();
        }
 }
}