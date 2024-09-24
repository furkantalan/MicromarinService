using MediatR;
using Micromarin.Application.Commands.Customers;
using Micromarin.Domain.Interfaces;
using AutoMapper;
using Micromarin.Application.Interfaces.Repositories;


namespace Micromarin.Application.Handlers.Command.Customer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, bool>
{
    private readonly IUnitOfWork<ICustomerRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(IUnitOfWork<ICustomerRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Entities.Customer>(request.CreateCustomerDto);
        await _unitOfWork.Repository.AddAsync(customer);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}
