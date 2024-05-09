
using System;

namespace AmazonApis.Errors
{
    public class ApiResponse
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            StatusCode = statusCode;
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            var Message = statusCode switch
            {
                400 => "you have made,A Bad Request",
                401 => "you are not Authorized",
                404 => "Resource was not Found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate.Hate leads to career change",
                _ => null
            };
            return Message;
        }
    }
}
