using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Customers;
using Micromarin.Application.DTOs.GetDtos;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;

namespace Micromarin.Application.Handlers.Query.Customer;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerDto>
{
    private readonly IUnitOfWork<ICustomerRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(IUnitOfWork<ICustomerRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetCustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository.GetByIdAsync(request.Id);
        if (customer == null)
        {
            throw new Exception("Customer not found.");
        }

        var mappingCustomer = _mapper.Map<GetCustomerDto>(customer);
        return mappingCustomer;
    }
}
