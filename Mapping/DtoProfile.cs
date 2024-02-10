using AparmentSystemAPI.Apartment;
using AparmentSystemAPI.Flats.DTOs;
using AparmentSystemAPI.Models.DTOs;
using AparmentSystemAPI.Payments;
using AparmentSystemAPI.Payments.DTOs;
using AutoMapper;

namespace AparmentSystemAPI.Mapping
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {  
            CreateMap<FlatDto,Flat>();
          //  CreateMap<PaymentDto, Payment>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<Flat, FlatDto>();
            CreateMap<AddFlatRequestDto, Flat>();

            // map addpaymentrequestdto to payment
            CreateMap<AddPaymentRequestDto, Payment>();
          
            
            // CreateMap<Flat, CreateFlatWithoutUserRequestDto>();            
        }

    }
}
