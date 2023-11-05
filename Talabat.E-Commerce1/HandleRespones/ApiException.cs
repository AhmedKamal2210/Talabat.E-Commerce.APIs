
namespace Talabat.E_Commerce1.HandleRespones
{
    public class ApiException : ApiResponse
    {
        public string Details { get; set; }
        public ApiException(int statusCode, string details = null, string message = null)
            : base(statusCode, message)
        {
            Details = details;
        }
    }
}
