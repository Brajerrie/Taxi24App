using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Taxi24App.Helper;

namespace Taxi24App.Common
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            string exceptionMessage = context.Exception.InnerException?.Message ?? context.Exception.Message;
            string rcode = "005";

            // Log the exception
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            int statusCode;
            string errorMessage;

            // Determine the type of exception and set the response accordingly
            if (context.Exception is ArgumentException || context.Exception is ArgumentNullException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorMessage = "Bad Request";
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                rcode = "403";
                statusCode = (int)HttpStatusCode.Unauthorized;
                errorMessage = "Unauthorized";
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                errorMessage = "Internal Server Error. Please contact your administrator.";
            }

            context.Result = new ObjectResult(new ApiResponse
            {
                ResponseCode = rcode,
                Message = errorMessage,
                Data = exceptionMessage
            })
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
