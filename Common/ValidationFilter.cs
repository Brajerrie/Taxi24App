using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using Taxi24App.Helper;

namespace Taxi24App.Common
{
    public class ValidateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ApiResponse
                {
                    ResponseCode = "422",
                    Data = context.ModelState,
                    Message = "One or more validation errors occurred."
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
