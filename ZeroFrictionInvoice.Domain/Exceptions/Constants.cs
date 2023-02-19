using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFrictionInvoice.Domain.Exceptions
{
    public static class Constants
    {
        public static class Errors
        {
            public const string InvalidInvoiceNumber = "Invalid invoice number";
            public const string InvoiceNotFound = "Invoice doesn't exist in the system";
            public const string InvoiceExisting = "Invoice number is already exisiting in the system";            
        }

        public static class ErrorCode
        {
            public const string InvalidInvoiceNumber = "1001";
            public const string InvoiceNotFound = "1002";
            public const string InvoiceExisting = "1003";            
        }
    }
}
