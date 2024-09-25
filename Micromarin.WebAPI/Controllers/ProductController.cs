using MediatR;
using Micromarin.Application.Commands.Products;
using Micromarin.Application.DTOs.CreateDtos;
using Micromarin.Application.DTOs.UpdateDtos;
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

    [HttpPut]
    [Route("UpdateProduct")]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto request)
    {
        var command = new UpdateProductCommand
        {
            UpdateProductDto = request
        };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete]
    [Route("RemoveProduct/{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var command = new RemoveProductCommand(id);
        var response = await _mediator.Send(command);
        if (response)
        {
            return Ok("Product removed successfully.");
        }
        return NotFound("Product not found.");
    }

    [HttpGet]
    [Route("GetProductById")]
    public async Task<IActionResult> GetProductById([FromQuery] GetProductByIdQuery request)
    {
        var response = await _mediator.Send(request);
        if (response != null)
        {
            return Ok(response);
        }
        return NotFound("Product not found.");
    }
}
