using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFrictionInvoice.Domain.Exceptions;
using ZeroFrictionInvoice.Infra;
using ZeroFrictionInvoice.Models;
using ZeroFrictionInvoice.Models.Dto;

namespace ZeroFrictionInvoice.Domain.Services
{
    public class InvoiceService: IInvoiceService
    {
        private readonly InvoiceContext dbContext;
        private readonly IMapper mapper;
        public InvoiceService(InvoiceContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task CreateInvoiceAsync(InvoiceModel invoiceModel)
        {
            if (invoiceModel is null)
                throw new Exception();

            var invoice = await dbContext.Invoices.FirstAsync(x => x.InvoiceNumber == invoiceModel.InvoiceNumber);

            if (invoice is not null)
                throw new BusinessException(Constants.ErrorCode.InvoiceExisting, Constants.Errors.InvoiceExisting);

            await dbContext.AddAsync(
                mapper.Map<InvoiceModel, Invoice>(invoiceModel, new Invoice() { 
                    Id = Guid.NewGuid().ToString().ToLower() 
                }));            

                await dbContext.SaveChangesAsync();            
        }

        public async Task UpdateInvoiceAsync(string invoiceNumber, InvoiceModel invoiceModel)
        {
            var invoice = await dbContext.Invoices.FirstAsync(x => x.InvoiceNumber == invoiceNumber);

            if (invoice is null)
                throw new BusinessException(Constants.ErrorCode.InvoiceNotFound, Constants.Errors.InvoiceNotFound);

            mapper.Map<InvoiceModel, Invoice>(invoiceModel, invoice);          

            dbContext.SaveChanges();
        }

        public async Task<List<InvoiceModel>> GetAllInvoicesAsync()
        {
            var invoices = await dbContext.Invoices.ToListAsync();

            var getInvoices = mapper.Map<List<Invoice>, List<InvoiceModel>>(invoices);                    

            return getInvoices;
        }

        public async Task<InvoiceModel> GetInvoiceByNumberAsync(string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
                throw new BusinessException(Constants.ErrorCode.InvalidInvoiceNumber, Constants.Errors.InvalidInvoiceNumber);

            var invoice = await dbContext.Invoices.Where(x => x.InvoiceNumber == invoiceNumber).FirstAsync();

            var getInvoices = mapper.Map<Invoice, InvoiceModel>(invoice);

            return getInvoices;
        }
    }
}
