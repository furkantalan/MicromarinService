using MediatR;
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
}
