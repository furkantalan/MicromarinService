using MediatR;
using Micromarin.Application.Commands.Products;
using Microsoft.AspNetCore.Mvc;



namespace Micromarin.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }


    //[HttpPost]
    //[Route("CreateProduct")]
    //public async Task<IActionResult> Create([FromBody] CreateProductCommand.Request request)
    //{
    //    var response = await _mediator.Send(request);
    //    return Ok(response);
    //}


}
