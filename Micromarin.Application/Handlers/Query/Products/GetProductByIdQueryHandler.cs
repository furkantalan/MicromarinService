using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Products;
using Micromarin.Application.DTOs.GetDtos;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;

namespace Micromarin.Application.Handlers.Query.Products;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductDto>
{
    private readonly IUnitOfWork<IProductRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork<IProductRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        var mappingProduct = _mapper.Map<GetProductDto>(product);
        return mappingProduct;
    }
}
