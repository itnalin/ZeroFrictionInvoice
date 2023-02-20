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
            CreateMap<InvoiceSaveModel, Invoice>()
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));                

            CreateMap<InvoiceLineSaveModel, InvoiceLine>();

            CreateMap<Invoice, InvoiceGetModel>();

            CreateMap<InvoiceLine, InvoiceLineGetModel>();           
            
        }
    }
}
