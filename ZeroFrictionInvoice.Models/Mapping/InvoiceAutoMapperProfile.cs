using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFrictionInvoice.Models.Dto;

namespace ZeroFrictionInvoice.Models.Mapping
{
    public class InvoiceAutoMapperProfile: Profile
    {
        public InvoiceAutoMapperProfile()
        {
            CreateMap<InvoiceModel, Invoice>()
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

            CreateMap<InvoiceLineModel, InvoiceLine>();
            CreateMap<Invoice, InvoiceModel>();
            CreateMap<InvoiceLine, InvoiceLineModel>();
        }
    }
}
