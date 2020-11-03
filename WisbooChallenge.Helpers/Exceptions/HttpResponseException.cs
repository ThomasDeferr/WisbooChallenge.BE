using System;
using System.Net;

namespace WisbooChallenge.Helpers.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public string Error { get; set; }



        public HttpResponseException() { }

        public HttpResponseException(string message)
            : base(message) { }

        public HttpResponseException(string message, Exception inner) 
            : base(message, inner) { }


        public HttpResponseException(HttpStatusCode statusCode) 
            : this(statusCode, null, null) { }

        public HttpResponseException(HttpStatusCode statusCode, string message)
            : this(statusCode, message, null) { }

        public HttpResponseException(HttpStatusCode statusCode, string message, string error)
            : base(message)
        {
            StatusCode = statusCode;
            Error = error;
        }
    }
}