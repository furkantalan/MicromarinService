using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Products;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;

namespace Micromarin.Application.Handlers.Command.Products;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IUnitOfWork<IProductRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork<IProductRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository.GetByIdAsync(request.UpdateProductDto.Id);
        if (product == null)
        { return false; }

        var updatedProduct = _mapper.Map(request.UpdateProductDto, product);
        await _unitOfWork.Repository.UpdateAsync(updatedProduct);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}
