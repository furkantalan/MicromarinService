using AutoMapper;
using MediatR;
using Micromarin.Application.Commands.Orders;
using Micromarin.Application.DTOs.GetDtos;
using Micromarin.Application.Interfaces.Repositories;
using Micromarin.Domain.Interfaces;

namespace Micromarin.Application.Handlers.Query.Orders;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderDto>
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
        var order = await _unitOfWork.Repository.GetByIdAsync(request.Id);
        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        var mappingOrder = _mapper.Map<GetOrderDto>(order);
        return mappingOrder;
    }
}
