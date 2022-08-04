using Microsoft.AspNetCore.Http;

namespace AuthServer.Extensions
{
    /// <summary>
    /// Http Response Extensions
    /// </summary>
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Add Error
        /// </summary>
        /// <param name="response"></param>
        /// <param name="message"></param>
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            // CORS
            response.Headers.Add("access-control-expose-headers", "Application-Error");
        }
    }
}