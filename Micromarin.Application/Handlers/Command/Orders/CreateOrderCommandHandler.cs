using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Orders;
using Micromarin.Application.Entities;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;


namespace Micromarin.Application.Handlers.Command.Orders;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
{
    private readonly IUnitOfWork<IOrderRepository> _unitOfWork;
    private readonly IUnitOfWork<IProductRepository> _productRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IUnitOfWork<IOrderRepository> unitOfWork, IMapper mapper, IUnitOfWork<IProductRepository> productRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerId = request.CreateOrderDto.CustomerId,
            Status = request.CreateOrderDto.Status,
            Products = new List<Product>(),
            TotalAmount = request.CreateOrderDto.TotalAmount
        };

        foreach (var productDto in request.CreateOrderDto.Products)
        {
            // Veritabanında bu product ID'ye sahip ürün var mı kontrol edin
            var existingProduct = await _productRepository.Repository.GetByIdAsync(productDto.Id);

            if (existingProduct != null)
            {
                // Eğer ürün zaten varsa, siparişe ekleyin
                order.Products.Add(existingProduct);
            }
            else
            {
                throw new Exception("product not found.");
            }
        }

        await _unitOfWork.Repository.AddAsync(order);
        await _unitOfWork.CompleteAsync();

        return true;
    }

}
