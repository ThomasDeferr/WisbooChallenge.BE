using System.Net;
using WisbooChallenge.Helpers.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WisbooChallenge.Helpers.Exceptions
{
    public class HttpResponseExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                HttpResponseError apiError = null;

                if (context.Result is ObjectResult objectResult)
                {
                    HttpStatusCode statusCode = ((HttpStatusCode?)objectResult.StatusCode).GetValueOrDefault(HttpStatusCode.InternalServerError);
                    apiError = new HttpResponseError(status: statusCode, message: objectResult.Value?.ToString());
                }
                else if (context.Result is StatusCodeResult statusCodeResult)
                {
                    apiError = new HttpResponseError(status: (HttpStatusCode)statusCodeResult.StatusCode);
                }

                if (!apiError.StatusCode.IsSuccess())
                {
                    context.Result = new ObjectResult(apiError)
                    {
                        StatusCode = apiError.StatusCode.GetValue()
                    };
                }
            }
        }
    }
}