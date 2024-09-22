using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Customers;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;



namespace Micromarin.Application.Handlers.Command.Customer;

/// <summary>
/// Command to remove a customer by Id.
/// </summary>  
public class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommand, bool>
{
    private readonly IUnitOfWork<ICustomerRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public RemoveCustomerCommandHandler(IUnitOfWork<ICustomerRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository.GetByIdAsync(request.Id);
        if (customer == null)
        { return false; }

        await _unitOfWork.Repository.RemoveAsync(request.Id);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}

