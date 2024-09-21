using MediatR;
using Micromarin.Domain.Controllers;
using Micromarin.Domain.Interfaces.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Micromarin.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : BaseApiController
{
    public ProductController(IMediator mediator, IErrorResponseFactory errorResponseFactory) : base(mediator, errorResponseFactory)
    {
    }
}
