

namespace Micromarin.Domain.Models;

public class Response<T>
{
    public T Data { get; private set; }
    public bool Success { get; private set; }
    public string Message { get; private set; }
    public int StatusCode { get; private set; }
    public List<string> Errors { get; private set; }
    public int ErrorCode { get; private set; } // Hata kodunu string olarak tutuyoruz

    private Response(T data, bool success, string message, int statusCode, int errorCode = default, List<string> errors = null)
    {
        Data = data;
        Success = success;
        Message = message;
        StatusCode = statusCode;
        ErrorCode = errorCode;
        Errors = errors ?? new List<string>();
    }

    public static Response<T> SuccessResponse(T data, string message = "Operation successful", int statusCode = 200)
    {
        return new Response<T>(data, true, message, statusCode);
    }
    public static Response<T> SuccessResponse(string message = "Operation successful", int statusCode = 200)
    {
        return new Response<T>(default, true, message, statusCode);
    }

    public static Response<T> ErrorResponse(string message, int statusCode = 400, int errorCode = default, List<string> errors = null)
    {
        return new Response<T>(default, false, message, statusCode, errorCode, errors);
    }

    public static Response<T> ErrorResponse(Exception ex, int statusCode = 500)
    {
        var errors = new List<string> { ex.Message };
        if (ex.InnerException != null)
        {
            errors.Add(ex.InnerException.Message);
        }
        return new Response<T>(default, false, ex.Message, statusCode, errors: errors);
    }
}