using MediatR;
using Micromarin.Application.Commands.Customers;
using Micromarin.Domain.Interfaces;
using AutoMapper;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Models;


namespace Micromarin.Application.Handlers.Command.Customer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand.Request, CreateCustomerCommand.Response>
{
    private readonly IUnitOfWork<ICustomerRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(IUnitOfWork<ICustomerRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateCustomerCommand.Response> Handle(CreateCustomerCommand.Request request, CancellationToken cancellationToken)
    {
        var result = new CreateCustomerCommand.Response(false);

        try
        {
            var customer = _mapper.Map<Entities.Customer>(request);
            await _unitOfWork.Repository.AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            result = new CreateCustomerCommand.Response(true);
        }
        catch (Exception ex)
        {
            result = new CreateCustomerCommand.Response(false);
        }
        return result;
    }
}
