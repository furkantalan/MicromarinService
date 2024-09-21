using MediatR;
using Micromarin.Domain.Enums;
using Micromarin.Domain.Interfaces.General;
using Micromarin.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Micromarin.Domain.Controllers;


/// <summary>
/// Base controller
/// </summary>
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected IMediator _mediator;
    protected readonly IErrorResponseFactory _errorResponseFactory;

    /// <summary>
    /// It is for getting the Mediator instance creation process from the base controller.
    /// </summary>
    public BaseApiController(IMediator mediator, IErrorResponseFactory errorResponseFactory)
    {
        _mediator = mediator;
        _errorResponseFactory = errorResponseFactory;
    }

    protected Response<T> TryParseObjectId<T>(string id, out ObjectId objectId)
    {
        objectId = ObjectId.Empty;
        if (string.IsNullOrWhiteSpace(id))
        {
            return _errorResponseFactory.CreateErrorResponse<T>(ErrorCodes.Required, 404, "Id");
        }

        try
        {
            objectId = ObjectId.Parse(id);
            return Response<T>.SuccessResponse();
        }
        catch (FormatException)
        {
            return _errorResponseFactory.CreateErrorResponse<T>(ErrorCodes.InvalidFormat, 400, "Id");
        }
    }
}