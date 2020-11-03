using System;
using System.Linq;
using System.Net;
using WisbooChallenge.Helpers.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace WisbooChallenge.Helpers.Exceptions
{
    public class HttpResponseError
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Trace { get; set; }


        public HttpResponseError(HttpStatusCode status = HttpStatusCode.InternalServerError, string message = null, string error = null, string trace = null)
        {
            this.StatusCode = status;
            this.Error = error ?? status.GetDefaultExceptionMessage();
            this.Message = message ?? status.GetDefaultMessage();
            this.Trace = trace;
        }

        public HttpResponseError(ModelStateDictionary modelState, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            this.StatusCode = status;
            this.Error = status.GetDefaultExceptionMessage();
            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
            {
                this.Message = String.Join(" ", modelState.SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage));
            }
        }
    }
}