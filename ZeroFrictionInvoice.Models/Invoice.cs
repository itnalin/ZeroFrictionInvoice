using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFrictionInvoice.Models
{
    public class Invoice
    {
        public string Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
