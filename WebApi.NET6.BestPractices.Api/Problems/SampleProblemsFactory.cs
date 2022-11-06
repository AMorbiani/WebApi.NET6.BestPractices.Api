using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;

namespace BestPractices.API.Problems
{
    public class SampleProblemsFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;
        public SampleProblemsFactory(IOptions<ApiBehaviorOptions> options)
        {
            _options = options?.Value;
        }

        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
        {
            var problem = new ProblemDetails
            {
                Detail = detail,
                Instance = instance,
                Status = statusCode ?? 500,
                Title = title,
                Type = type,
            };
            if (_options != null)
            {
                if (_options.ClientErrorMapping.TryGetValue(statusCode.Value, out var clientErrorData))
                {
                    problem.Title ??= clientErrorData.Title;
                    problem.Type ??= clientErrorData.Link;
                }
            }
            var traceId = httpContext?.TraceIdentifier;
            if (traceId != null) problem.Extensions["traceId"] = traceId;
            problem.Extensions.Add("My Custom Value", "Value");
            problem.Extensions.Add("My Custom Value2", new { PropertyOne = "Test" });
            return problem;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
        {
            return new ValidationProblemDetails(modelStateDictionary)
            {
                Detail = detail,
                Instance = instance,
                Status = statusCode,
                Title = title,
                Type = type,
            };
        }
    }
}
