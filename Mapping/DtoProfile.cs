using AparmentSystemAPI.Models.Flats;
using AparmentSystemAPI.Models.Flats.DTOs;
using AparmentSystemAPI.Models.Payments;
using AparmentSystemAPI.Models.Payments.DTOs;
using AutoMapper;

namespace AparmentSystemAPI.Mapping
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {  
            CreateMap<FlatDto,Flat>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<Flat, FlatDto>();
            CreateMap<AddFlatRequestDto, Flat>();
            CreateMap<AddPaymentRequestDto, Payment>();

      
        }
    }
}
