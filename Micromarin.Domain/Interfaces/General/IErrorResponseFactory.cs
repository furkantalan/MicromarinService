using Micromarin.Domain.Enums;
using Micromarin.Domain.Models;

namespace Micromarin.Domain.Interfaces.General;

public interface IErrorResponseFactory
{
    Response<T> CreateErrorResponse<T>(ErrorCodes errorCode, int statusCode, params object[] args);
    Response<T> CreateErrorResponse<T>(Exception ex, int statusCode = 500);
}
