using MediatR;
using Micromarin.Domain.Controllers;
using Micromarin.Domain.Interfaces.General;
using Microsoft.AspNetCore.Mvc;



namespace Micromarin.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : BaseApiController
{
    public OrderController(IMediator mediator, IErrorResponseFactory errorResponseFactory) : base(mediator, errorResponseFactory)
    {
    }
}
