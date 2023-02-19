using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZeroFrictionInvoice.Domain.Services;
using ZeroFrictionInvoice.Infra;
using ZeroFrictionInvoice.Models.Dto;
using ZeroFrictionInvoice.Models.Mapping;
using ZeroFrictionInvoice.Test.Utilities;

namespace ZeroFrictionInvoice.Domain.Test.Services
{
    public class InvoiceServiceTest: IClassFixture<IMapper>
    {
        private readonly IInMemoryDatabaseFactory<InvoiceContext> inMemoryDatabaseFactory;
        private readonly IMapper _mapper;
        public InvoiceServiceTest()
        {
            inMemoryDatabaseFactory = new InMemoryDatabaseFactory<InvoiceContext>();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new InvoiceAutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task CreateNewInvoice_ShouldCreateNewInvoice()
        {           

            //Arrange
            var dbContext = inMemoryDatabaseFactory.Create("InvoiceInMemoryDb");
           
            IInvoiceService invoice = new InvoiceService(dbContext, _mapper);
            SeedDatabase(dbContext);

            var param = new InvoiceModel()
            {
                InvoiceNumber = "INV102",
                Date = DateTime.UtcNow,
                Description = "test100",
                TotalAmount = 100,
                InvoiceLines = new HashSet<InvoiceLineModel>()
                        {
                            new InvoiceLineModel()
                            {
                                Amount= 100,
                                Quantity= 100,
                                UnitPrice= 100,
                                LineAmount= 100
                            }
                        }
            };

            //Act
            await invoice.CreateInvoiceAsync(param);
            var result = await invoice.GetInvoiceByNumberAsync(param.InvoiceNumber);

            //Assert
            result.Should().NotBeNull();
            result.Date.Should().Be(param.Date);
            result.Description.Should().Be(param.Description);
            result.TotalAmount.Should().Be(param.TotalAmount);
            result.InvoiceLines.Should().NotBeNull();
            result.InvoiceLines.Count().Should().Be(param.InvoiceLines.Count());
        }

        private void SeedDatabase(InvoiceContext dbContext)
        {
            // SupplierDetail            
            //dbContext.Suppliers.AddAsync(new SupplierDetail()
            //{
            //    Id = "5645321H-06b9-4419-9c05-d56a9401c82b",
            //    ReferenceNumber = 101,
            //    StatusId = (int)SupplierStatus.Registered,
            //    ExternalSupplierCode = "Supplier123",
            //    CompanyDetails = new SupplierCompanyDetails() { CompanyName = "abc2" }
            //});

            dbContext.AddAsync(
                new Models.Invoice
                {
                    Id = Guid.NewGuid().ToString().ToLower(),
                    InvoiceNumber = Guid.NewGuid().ToString().ToLower(),
                    Date = DateTime.UtcNow,
                    Description = "test100",
                    TotalAmount = 100,
                    InvoiceLines = new HashSet<Models.InvoiceLine>()
                    {
                            new Models.InvoiceLine()
                            {
                                Amount= 100,
                                Quantity= 100,
                                UnitPrice= 100,
                                LineAmount= 100
                            }
                    }
                });

            dbContext.SaveChanges();
        }
    }

}
