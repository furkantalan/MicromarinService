using MediatR;
using Micromarin.Application.Commands.Customers;
using Micromarin.Application.DTOs.UpdateDtos;
using Microsoft.AspNetCore.Mvc;


namespace Micromarin.WebAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    protected IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("CreateCustomer")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand.Request request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    [Route("UpdateCustomer")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerDto request)
    {
        var command = new UpdateCustomerCommand
        {
            UpdateCustomerDto = request
        };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete]
    [Route("RemoveCustomer/{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var command = new RemoveCustomerCommand(id);
        var response = await _mediator.Send(command);
        if (response)
        {
            return Ok("Customer removed successfully.");
        }
        return NotFound("Customer not found.");
    }

    [HttpGet]
    [Route("GetCustomerById")]
    public async Task<IActionResult> GetCustomerById([FromQuery] GetCustomerByIdQuery request)
    {
        var response = await _mediator.Send(request);
        if (response != null)
        {
            return Ok(response);
        }
        return NotFound("Customer not found.");
    }
}

