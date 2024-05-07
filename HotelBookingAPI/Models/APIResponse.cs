using System.Net;

namespace HotelBookingAPI.Models
{
    //This is a generic class for returning different type of responses.
    public class APIResponse<T>
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public object? Error { get; set; }

        //Constructor for the success response.
        public APIResponse(T data, string message="", HttpStatusCode statuCode = HttpStatusCode.OK)
        {
            Success = true;
            StatusCode = statuCode;
            Message = message;
            Data = data;
            Error = null;            
        }

        //Constructor for the failed response.
        public APIResponse(HttpStatusCode statusCode, string message, object? error = null)
        {
            Success = false;
            StatusCode = statusCode;
            Message = message;
            Data = default(T);
            Error = error;
        }

    }
}
