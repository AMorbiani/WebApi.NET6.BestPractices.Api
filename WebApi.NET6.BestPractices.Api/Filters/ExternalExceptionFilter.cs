using BestPractices.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BestPractices.API.Filters
{
    // FILTER FOR EXCEPTIONS
    public class ExternalExceptionFilter : IActionFilter, IOrderedFilter
    {
        // Execute at the end (high value = lower priority)
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        // Manage all ArgumentException
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ArgumentException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.Message)
                {
                    StatusCode = StatusCodes.Status424FailedDependency,
                };

                context.ExceptionHandled = true;
            };

        }
    }
}
