using BestPractices.API.Controllers.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace BestPractices.Api.Attributes
{
    public class ElapsedTimeAttributeAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($"REQUEST API - {context.ActionDescriptor.DisplayName}.");
            var timer = new Stopwatch();
            timer.Start();
            await next();
            timer.Stop();
            Console.WriteLine($"RESPONSE API - {context.ActionDescriptor.DisplayName} - Elapsed Time: {timer.ElapsedMilliseconds}.");
        }
    }
}
