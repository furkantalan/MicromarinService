﻿

using MediatR;

namespace Micromarin.Application.Commands.Orders;

public class RemoveOrderCommand : IRequest<bool>
{
    public Guid Id { get; }

    public RemoveOrderCommand(Guid id)
    {
        Id = id;
    }
}
