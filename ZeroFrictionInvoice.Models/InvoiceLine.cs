using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZeroFrictionInvoice.Models
{
    public class InvoiceLine
    {          
        public int Amount { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int LineAmount { get; set; }        
    }
}
