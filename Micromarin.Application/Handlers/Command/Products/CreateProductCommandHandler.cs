using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Products;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;


namespace Micromarin.Application.Handlers.Command.Products;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
{
    private readonly IUnitOfWork<IProductRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork<IProductRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Entities.Product>(request.CreateProductDto);
        await _unitOfWork.Repository.AddAsync(product);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}
