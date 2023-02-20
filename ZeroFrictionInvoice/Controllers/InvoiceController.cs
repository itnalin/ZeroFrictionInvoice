using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Drawing;
using ZeroFrictionInvoice.Domain.Services;
using ZeroFrictionInvoice.Models.Dto;

namespace ZeroFrictionInvoice.API.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{api-version:apiVersion}/[controller]")]    
    [Consumes("application/json")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(
            IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet, Route("{number}")]
        [Produces("application/json", Type = typeof(InvoiceGetModel))]
        [ProducesResponseType(typeof(InvoiceGetModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInvoiceByNumberAsync(string number)
        {
            return Ok(await _invoiceService.GetInvoiceByNumberAsync(number));
        }

        [HttpGet, Route("all")]
        [Produces("application/json", Type = typeof(List<InvoiceGetModel>))]
        [ProducesResponseType(typeof(InvoiceGetModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllInvoicesAsync()
        {
            return Ok(await _invoiceService.GetAllInvoicesAsync());
        }        

        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateInvoiceAsync(InvoiceSaveModel invoice)
        {
            await _invoiceService.CreateInvoiceAsync(invoice);

            return Ok();
        }

        [HttpPut, Route("{invoiceNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateInvoiceAsync(string invoiceNumber, [FromBody] InvoiceSaveModel invoice)
        {
            await _invoiceService.UpdateInvoiceAsync(invoiceNumber, invoice);

            return Ok();
        }
    }
}
