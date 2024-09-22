using AutoMapper;
using Micromarin.Application.Commands.Customers;
using Micromarin.Application.Entities;

namespace Micromarin.Application.Mappings;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateCustomerCommand.Request, Customer>().ReverseMap();

    }
}
