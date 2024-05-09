namespace AmazonApis.Errors
{
    public class ApiValidationErrorReponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorReponse() : base(400)
        {
            Errors = new List<string>();
        }
    }
}
