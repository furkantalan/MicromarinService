using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Customers;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;

namespace Micromarin.Application.Handlers.Command.Customer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
{
    private readonly IUnitOfWork<ICustomerRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(IUnitOfWork<ICustomerRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository.GetByIdAsync(request.UpdateCustomerDto.Id);
        if (customer == null)
        { return false; }

        var updatedCustomer = _mapper.Map(request.UpdateCustomerDto, customer);
        await _unitOfWork.Repository.UpdateAsync(updatedCustomer);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}