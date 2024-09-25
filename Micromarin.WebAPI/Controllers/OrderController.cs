using MediatR;
using Micromarin.Application.Commands.Orders;
using Micromarin.Application.DTOs.CreateDtos;
using Micromarin.Application.DTOs.UpdateDtos;
using Microsoft.AspNetCore.Mvc;


namespace Micromarin.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    protected IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("CreateOrder")]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto request)
    {
        var command = new CreateOrderCommand { CreateOrderDto = request };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [Route("UpdateOrder")]
    public async Task<IActionResult> Update([FromBody] UpdateOrderDto request)
    {
        var command = new UpdateOrderCommand
        {
            UpdateOrderDto = request
        };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete]
    [Route("RemoveOrder/{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var command = new RemoveOrderCommand(id);
        var response = await _mediator.Send(command);
        if (response)
        {
            return Ok("Order removed successfully.");
        }
        return NotFound("Order not found.");
    }

    [HttpGet]
    [Route("GetOrderById")]
    public async Task<IActionResult> GetOrderById([FromQuery] GetOrderByIdQuery request)
    {
        var response = await _mediator.Send(request);
        if (response != null)
        {
            return Ok(response);
        }
        return NotFound("Order not found.");
    }
}
