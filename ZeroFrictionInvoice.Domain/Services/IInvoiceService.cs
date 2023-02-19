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
        Task CreateInvoiceAsync(InvoiceModel invoiceModel);
        Task UpdateInvoiceAsync(string invoiceNumber, InvoiceModel invoiceModel);
        Task<List<InvoiceModel>> GetAllInvoicesAsync();
        Task<InvoiceModel> GetInvoiceByNumberAsync(string invoiceNumber);
    }
}
