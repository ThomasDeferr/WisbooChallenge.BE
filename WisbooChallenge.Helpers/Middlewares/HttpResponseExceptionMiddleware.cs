using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Hosting;
using WisbooChallenge.Helpers.Exceptions;
using WisbooChallenge.Helpers.Extensions;

namespace WisbooChallenge.Helpers.Middlewares
{
    public class HttpResponseExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;

        public HttpResponseExceptionMiddleware(RequestDelegate next, IHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            HttpResponseError response = null;

            try
            {
                await _next.Invoke(context);

                response = new HttpResponseError(status: (HttpStatusCode)context.Response.StatusCode);
            }
            catch (HttpResponseException ex)
            {
                response = new HttpResponseError(status: ex.StatusCode, message: ex.Message);
                if (_environment.IsDevelopment())
                {
                    response.Error = ex.Error;
                    response.Trace = ex.StackTrace;
                }

                Console.WriteLine($"HttpResponseException endpoint {context.Request.Method}:{context.Request.Path.Value} {response.Error} {response.Trace}");
            }
            catch (Exception ex)
            {
                response = new HttpResponseError(status: HttpStatusCode.InternalServerError, message: ex.Message);
                if (_environment.IsDevelopment())
                {
                    response.Trace = ex.StackTrace;
                }

                Console.WriteLine($"Exception endpoint {context.Request.Method}:{context.Request.Path} {response.Error} {response.Trace}");
            }
            finally
            {
                if (!context.Response.HasStarted && response != null && response.StatusCode != HttpStatusCode.NoContent)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = response.StatusCode.GetValue();

                    string jsonResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver()
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    });

                    await context.Response.WriteAsync(jsonResponse);
                }
            }
        }
    }
}