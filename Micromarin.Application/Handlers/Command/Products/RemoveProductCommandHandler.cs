using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Products;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;

namespace Micromarin.Application.Handlers.Command.Products;

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, bool>
{
    private readonly IUnitOfWork<IProductRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public RemoveProductCommandHandler(IUnitOfWork<IProductRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var product  = await _unitOfWork.Repository.GetByIdAsync(request.Id);
        if (product == null)
        { return false; }

        await _unitOfWork.Repository.RemoveAsync(request.Id);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}
