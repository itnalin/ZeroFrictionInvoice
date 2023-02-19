using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFrictionInvoice.Test.Utilities
{
    public class InMemoryDatabaseFactory<TContext> : IInMemoryDatabaseFactory<TContext>
        where TContext : DbContext
    {
        public TContext Create(string databaseName)
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            builder.UseInMemoryDatabase(databaseName: databaseName);

            var dbContext = (TContext)Activator.CreateInstance(typeof(TContext), builder.Options);
            // Delete existing db before creating a new one
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
