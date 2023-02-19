using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using ZeroFrictionInvoice.API.Extensions;
using ZeroFrictionInvoice.Domain.Services;
using ZeroFrictionInvoice.Infra;
using ZeroFrictionInvoice.Models.Mapping;
using ZeroFrictionInvoice.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<InvoiceContext>(options =>
options.UseCosmos(builder.Configuration.GetSection("CosmosDb:AccountEndpoint").Value,
builder.Configuration.GetSection("CosmosDb:AccountKey").Value,
databaseName: builder.Configuration.GetSection("CosmosDb:DatabaseName").Value));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<InvoiceAutoMapperProfile>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddVersionedApiExplorer(option =>
{
    option.GroupNameFormat = "'v'VVV";
    option.SubstituteApiVersionInUrl = true;
});
builder.Services.AddApiVersioning(o => o.ReportApiVersions = true);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
