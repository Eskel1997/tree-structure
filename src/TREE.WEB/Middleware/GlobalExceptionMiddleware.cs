using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Castle.Core.Internal;
using TREE.DB.Exceptions;

namespace TREE.WEB.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate request;
        private readonly ILogger<GlobalExceptionMiddleware> logger;

        public GlobalExceptionMiddleware(RequestDelegate request, ILogger<GlobalExceptionMiddleware> logger)
        {
            this.request = request;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.request(context);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            var errorCode = nameof(HttpStatusCode.InternalServerError);
            var statusCode = HttpStatusCode.InternalServerError;
            var message = exception.Message;

            if (exception is TreeException treeException)
            {
                statusCode = treeException.ErrorCode.StatusCode;
                errorCode = treeException.ErrorCode.Message;
                message = treeException.Message.IsNullOrEmpty() ? errorCode : treeException.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;
            var responseBody = JsonConvert.SerializeObject(new {errorCode, message});

            return context.Response.WriteAsync(responseBody);
        }
    }
}
