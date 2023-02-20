using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFrictionInvoice.Models;
using ZeroFrictionInvoice.Models.Dto;

namespace ZeroFrictionInvoice.Domain.Services
{
    public interface IInvoiceService
    {
        Task CreateInvoiceAsync(InvoiceSaveModel invoiceSaveModel);
        Task UpdateInvoiceAsync(string invoiceNumber, InvoiceSaveModel invoiceSaveModel);
        Task<List<InvoiceGetModel>> GetAllInvoicesAsync();
        Task<InvoiceGetModel> GetInvoiceByNumberAsync(string invoiceNumber);
    }
}
