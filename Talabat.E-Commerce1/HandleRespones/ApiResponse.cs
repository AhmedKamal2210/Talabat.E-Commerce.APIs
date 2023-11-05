namespace Talabat.E_Commerce1.HandleRespones
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefualtMessageForStatusCode(statusCode);
        }

        private string GetDefualtMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Requst",
                401 => "You are not authorized",
                404 => "Resourse not found !",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
