using MediatR;
using Micromarin.Application.Commands.Products;
using Micromarin.Application.DTOs.CreateDtos;
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


    [HttpPost]
    [Route("CreateProduct")]
    public async Task<IActionResult> Create([FromBody] CreateProductDto request)
    {
        var command = new CreateProductCommand { CreateProductDto = request };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


}
