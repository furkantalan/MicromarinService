using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Orders;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;


namespace Micromarin.Application.Handlers.Command.Orders;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IUnitOfWork<IOrderRepository> _unitOfWork;
    private readonly IUnitOfWork<IProductRepository> _productRepository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IUnitOfWork<IOrderRepository> unitOfWork, IMapper mapper, IUnitOfWork<IProductRepository> productRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Repository.GetByIdAsync(request.UpdateOrderDto.Id);
        if (order == null)
        {
            return false;
        }

        // DTO'dan Order'a diğer alanları eşle
        _mapper.Map(request.UpdateOrderDto, order);

        // Mevcut ürünleri ekleme ve çıkarma işlemleri
        var incomingProductIds = request.UpdateOrderDto.Products.Select(p => p.Id).ToHashSet();
        var existingProductIds = order.Products.Select(p => p.Id).ToHashSet();

        // Çıkarılacak ürünleri belirle
        var productsToRemove = order.Products.Where(p => !incomingProductIds.Contains(p.Id)).ToList();

        // Mevcut siparişten ürünleri çıkar
        foreach (var productToRemove in productsToRemove)
        {
            order.Products.Remove(productToRemove);
        }

        // Eklenmesi gereken ürünleri ekle
        foreach (var productDto in request.UpdateOrderDto.Products)
        {
            var existingProduct = await _productRepository.Repository.GetByIdAsync(productDto.Id);

            if (existingProduct != null && !existingProductIds.Contains(existingProduct.Id))
            {
                order.Products.Add(existingProduct);
            }
        }

        await _unitOfWork.Repository.UpdateAsync(order);
        await _unitOfWork.CompleteAsync();

        return true;
    }

}

