using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFrictionInvoice.Models.Dto
{
    public class InvoiceModel
    {
        [Required(ErrorMessage = "InvoiceNumber is required.")]
        public string InvoiceNumber { get; set; }
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "TotalAmount is required.")]
        public int TotalAmount { get; set; }
        [Required(ErrorMessage = "InvoiceLines is required.")]
        public ICollection<InvoiceLineModel> InvoiceLines { get; set; }
    }

    public class InvoiceLineModel
    {
        [Required(ErrorMessage = "Amount is required.")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "UnitPrice is required.")]
        public int UnitPrice { get; set; }
        [Required(ErrorMessage = "LineAmount is required.")]
        public int LineAmount { get; set; }
    }
}
