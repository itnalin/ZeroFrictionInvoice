using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ZeroFrictionInvoice.Models;

namespace ZeroFrictionInvoice.Infra
{
    public class InvoiceContext: DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options): base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
                        
        }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().OwnsMany(
     i => i.InvoiceLines,
     il =>
     {
         il.ToJsonProperty("InvoiceLines");         
         il.Property(p => p.Amount).ToJsonProperty("Amount");
         il.Property(p => p.Quantity).ToJsonProperty("Quantity");
         il.Property(p => p.UnitPrice).ToJsonProperty("UnitPrice");
         il.Property(p => p.LineAmount).ToJsonProperty("LineAmount");
     });
        }
        #endregion

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
    }
}
