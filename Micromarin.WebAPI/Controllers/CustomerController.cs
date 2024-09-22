using MediatR;
using Micromarin.Application.Commands.Customers;
using Micromarin.Domain.Controllers;
using Micromarin.Domain.Interfaces.General;
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
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand.Request request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

}
