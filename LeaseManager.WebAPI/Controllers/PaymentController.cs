using LeaseManager.WebAPI.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static LeaseManager.WebAPI.Application.DTOs.PaymentDTOs;

namespace LeaseManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
      {
         var payments = await _paymentService.GetAllAsync();
   return Ok(payments);
 }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
  {
        var payment = await _paymentService.GetByIdAsync(id);
      if (payment == null) return NotFound();
            return Ok(payment);
     }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentCreateDto dto)
        {
    var payment = await _paymentService.CreateAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PaymentUpdateDto dto)
      {
       var success = await _paymentService.UpdateAsync(id, dto);
   if (!success) return NotFound();
            return NoContent();
        }

      [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
  var success = await _paymentService.DeleteAsync(id);
    if (!success) return NotFound();
          return NoContent();
}
    }
}