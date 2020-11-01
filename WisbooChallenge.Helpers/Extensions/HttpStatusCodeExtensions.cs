using System.Net;

namespace WisbooChallenge.Helpers.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static int GetValue(this HttpStatusCode httpStatusCode)
        {
            return (int)httpStatusCode;
        }

        public static bool IsSuccess(this HttpStatusCode httpStatusCode)
        {
            return (httpStatusCode.GetValue() >= 200 && httpStatusCode.GetValue() <= 299);
        }

        public static string GetDefaultMessage(this HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return "Unauthorized Message";
                case HttpStatusCode.BadRequest:
                    return "BadRequest Message";
                case HttpStatusCode.NotFound:
                    return "NotFound Message";
                case HttpStatusCode.InternalServerError:
                    return "InternalServerError Message";
                default:
                    return "";
            }
        }

        public static string GetDefaultExceptionMessage(this HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return "Unauthorized Error";
                case HttpStatusCode.BadRequest:
                    return "BadRequest Error";
                case HttpStatusCode.NotFound:
                    return "NotFound Error";
                case HttpStatusCode.InternalServerError:
                    return "InternalServerError Error";
                default:
                    return "";
            }
        }
    }
}