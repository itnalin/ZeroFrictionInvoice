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

        public async Task CreateInvoiceAsync(InvoiceSaveModel invoiceSaveModel)
        {
            if (invoiceSaveModel is null)
                throw new Exception();

            var invoice = await dbContext.Invoices.Where(x => x.InvoiceNumber == invoiceSaveModel.InvoiceNumber).FirstOrDefaultAsync();

            if (invoice is not null)
                throw new BusinessException(Constants.ErrorCode.InvoiceExisting, Constants.Errors.InvoiceExisting);

            var mappedInvoice = mapper.Map<InvoiceSaveModel, Invoice>(invoiceSaveModel, new Invoice()
            {
                Id = Guid.NewGuid().ToString().ToLower()                
            });

            // Calculate Line Amount and Total Amount
            decimal total = 0;
            foreach(var line in mappedInvoice.InvoiceLines)
            {
                line.LineAmount = line.Quantity * line.UnitPrice;
                line.Amount = line.LineAmount;
                total += line.LineAmount;
            }

            mappedInvoice.TotalAmount = total;

            await dbContext.AddAsync(mappedInvoice);            

            await dbContext.SaveChangesAsync();            
        }

        public async Task UpdateInvoiceAsync(string invoiceNumber, InvoiceSaveModel invoiceSaveModel)
        {
            var invoice = await dbContext.Invoices.Where(x => x.InvoiceNumber == invoiceNumber).FirstOrDefaultAsync();

            if (invoice is null)
                throw new BusinessException(Constants.ErrorCode.InvoiceNotFound, Constants.Errors.InvoiceNotFound);

            // when an invoice number is going to be modified, it will check is there is an invoice with modified invoice numner
            if(invoiceNumber != invoiceSaveModel.InvoiceNumber)
            {
                var existingInvoice = await dbContext.Invoices.Where(x => x.InvoiceNumber == invoiceSaveModel.InvoiceNumber).FirstOrDefaultAsync();

                if(existingInvoice is not null)
                    throw new BusinessException(Constants.ErrorCode.InvoiceExisting, Constants.Errors.InvoiceExisting);
            }

            mapper.Map<InvoiceSaveModel, Invoice>(invoiceSaveModel, invoice);

            // Calculate Line Amount and Total Amount
            decimal total = 0;
            foreach (var line in invoice.InvoiceLines)
            {
                line.LineAmount = line.Quantity * line.UnitPrice;
                line.Amount = line.LineAmount;
                total += line.LineAmount;
            }

            invoice.TotalAmount = total;

            dbContext.SaveChanges();
        }

        public async Task<List<InvoiceGetModel>> GetAllInvoicesAsync()
        {
            var invoices = await dbContext.Invoices.ToListAsync();

            var getInvoices = mapper.Map<List<Invoice>, List<InvoiceGetModel>>(invoices);                    

            return getInvoices;
        }

        public async Task<InvoiceGetModel> GetInvoiceByNumberAsync(string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
                throw new BusinessException(Constants.ErrorCode.InvalidInvoiceNumber, Constants.Errors.InvalidInvoiceNumber);

            var invoice = await dbContext.Invoices.Where(x => x.InvoiceNumber == invoiceNumber).FirstOrDefaultAsync();

            var getInvoices = mapper.Map<Invoice, InvoiceGetModel>(invoice);

            return getInvoices;
        }
    }
}
