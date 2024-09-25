using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Orders;
using Micromarin.Application.DTOs.GetDtos;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Micromarin.Application.Handlers.Query.Orders;

internal class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderDto>
{
    private readonly IUnitOfWork<IOrderRepository> _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IUnitOfWork<IOrderRepository> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        // Order ve ilişkili Products verisini birlikte getiriyoruz
        var order = await _context.Orders
            .Include(o => o.Products) // İlgili Products verisini de dahil ediyoruz
            .FirstOrDefaultAsync(o => o.Id == request.Id); // Belirtilen ID'ye göre Order getiriyoruz

        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        // Gelen veriyi DTO'ya mapliyoruz
        var mappingOrder = _mapper.Map<GetOrderDto>(order);

        return mappingOrder;
    }
}
