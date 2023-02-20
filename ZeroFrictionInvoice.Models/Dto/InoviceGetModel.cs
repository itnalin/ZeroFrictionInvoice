using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFrictionInvoice.Models.Dto
{
    public class InvoiceGetModel
    {   
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<InvoiceLineGetModel> InvoiceLines { get; set; }
    }

    public class InvoiceLineGetModel
    {
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineAmount { get; set; }
    }
}
