using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFrictionInvoice.Models.Dto
{
    public class InvoiceSaveModel
    {
        [Required(ErrorMessage = "InvoiceNumber is required.")]
        public string InvoiceNumber { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }        
        
        [Required(ErrorMessage = "InvoiceLines is required.")]
        public ICollection<InvoiceLineSaveModel> InvoiceLines { get; set; }
    }

    public class InvoiceLineSaveModel
    {
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }
        
        [Required(ErrorMessage = "UnitPrice is required.")]
        public decimal UnitPrice { get; set; }
    }
}
