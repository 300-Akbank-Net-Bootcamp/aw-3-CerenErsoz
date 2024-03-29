using AutoMapper;
using VbApi.VbBusiness.Cqrs;
using VbApi.VbSchema;

using VbApi.VbData.Entity;

namespace VbApi.VbBusiness.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();

            CreateMap<AddressRequest, Address>();

            CreateMap<Address, AddressResponse>()
                .ForMember(dest => dest.CustomerName,
                    src => src.MapFrom(x => x.Customer.FirstName + " " + x.Customer.LastName));


            CreateMap<ContactRequest, Contact>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ContactRequest, ContactResponse>();
            CreateMap<Contact, ContactResponse>()
                .ForMember(dest => dest.CustomerName,
                    src => src.MapFrom(x => x.Customer.FirstName + " " + x.Customer.LastName));

            CreateMap<AccountRequest, Account>();
            CreateMap<Account, AccountResponse>()
                .ForMember(dest => dest.CustomerName,
                    src => src.MapFrom(x => x.Customer.FirstName + " " + x.Customer.LastName));
            CreateMap<Account, AccountResponse>()
                .ForMember(dest => dest.CustomerName,
                    src => src.MapFrom(x => x.Customer.FirstName + " " + x.Customer.LastName));


            CreateMap<AccountTransactionRequest, AccountTransaction>();
            CreateMap<AccountTransaction, AccountTransactionResponse>();

            CreateMap<AddressRequest, AddressResponse>();

            CreateMap<EftTransactionRequest, EftTransaction>();
            CreateMap<EftTransaction, EftTransactionResponse>();


        }
    }
}