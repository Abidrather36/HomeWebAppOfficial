namespace HomeWebApp.Application.Shared;

public class APIResponse<T>
{
    private readonly bool isSuccess;
    private readonly int statusCode;
    private readonly string message;
    private readonly T? result;

    public bool IsSuccess => isSuccess;
    public int StatusCode => statusCode;
    public string Message => message;
    public T? Result => result;

    public APIResponse(T? result, int statusCode = APIStatusCodes.OK, string message = "Success")
    {
        if ((int)statusCode < 100 || (int)statusCode >= 600)
        {
            throw new ArgumentException(APIMessages.InvaildStatusCode);
        }

        isSuccess = true;
        this.statusCode = statusCode;
        this.message = message;
        this.result = result;
    }

    public APIResponse(string message, int statusCode = APIStatusCodes.BadRequest)
    {
        if ((int)statusCode < 100 || (int)statusCode >= 600)
        {
            throw new ArgumentException(APIMessages.InvaildStatusCode);
        }

        isSuccess = false;
        this.statusCode = statusCode;
        this.message = message;
        this.result = default;
    }

    // Additional Constructors
    public APIResponse(T result, string message, int statusCode = APIStatusCodes.OK)
        : this(result, statusCode, message)
    {
    }

    public APIResponse(int statusCode = APIStatusCodes.OK, string message = "Success")
        : this(default, statusCode, message)
    {
    }

    // Static Factory Methods
    public static APIResponse<T> SuccessResponse(T? result, int statusCode = APIStatusCodes.OK, string message = "Success")
    {
        return new APIResponse<T>(result, statusCode, message);
    }

    public static APIResponse<T> ErrorResponse(string message = "Error", int statusCode = APIStatusCodes.BadRequest)
    {
        return new APIResponse<T>(message, statusCode);
    }
}
