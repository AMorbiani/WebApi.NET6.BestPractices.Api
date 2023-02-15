using MediatR;
using System.Diagnostics;
using System.Reflection;

namespace BestPractices.Api.Pipelines
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //Request
            _logger.LogInformation($"REQUEST API - {typeof(TRequest).Name}.");

            var timer = new Stopwatch();

            timer.Start();
            var response = await next();
            timer.Stop();

            //Response
            _logger.LogInformation($"RESPONSE API - {typeof(TRequest).Name} - Elapsed Time: {timer.ElapsedMilliseconds}.");

            return response;
        }
    }
}
