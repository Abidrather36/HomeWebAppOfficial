using HomeWebApp.Domain.Enums;

namespace HomeWebApp.Application.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }


        public T? Result { get; set; }


        public string? Message { get; set; }


        public StatusCode StatusCode { get; set; }


        public static ApiResponse<T> SuccesResponse(T result , string message="" ,StatusCode statusCode=StatusCode.OK)
        {
            return new ApiResponse<T>
            {
                Result = result,
                Message = message,
                StatusCode = statusCode,
                IsSuccess = true,
            };

        }

        public static ApiResponse<T> ErrorResponse(string message="",StatusCode statusCode=StatusCode.BadGateway)
        {
            return new ApiResponse<T>
            {
               
                Message = message,
                StatusCode = statusCode,
                IsSuccess = false,
            };
        }
    }
}
