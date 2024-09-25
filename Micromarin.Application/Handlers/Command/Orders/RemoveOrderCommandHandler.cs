using MediatR;
using Micromarin.Application.Commands.Orders;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;


namespace Micromarin.Application.Handlers.Command.Orders;

public class RemoveOrderCommandHandler : IRequestHandler<RemoveOrderCommand, bool>
{
    private readonly IUnitOfWork<IOrderRepository> _unitOfWork;

    public RemoveOrderCommandHandler(IUnitOfWork<IOrderRepository> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Repository.GetByIdAsync(request.Id);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {request.Id} not found.");
        }

        _unitOfWork.Repository.RemoveAsync(request.Id);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}
