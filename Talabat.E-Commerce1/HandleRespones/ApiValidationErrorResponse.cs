namespace Talabat.E_Commerce1.HandleRespones
{
    public class ApiValidationErrorResponse : ApiException
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse() : base(400)
        {

        }
    }
}
