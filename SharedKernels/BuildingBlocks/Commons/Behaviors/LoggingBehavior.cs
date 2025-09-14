using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Commons.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : 
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handling request of type {RequestType}", typeof(TRequest).Name);

        var timer = new Stopwatch();
        timer.Start();
        var response = await next();
        var timeTaken = timer.Elapsed;
        
        if(timeTaken.Seconds > 5)
        {
            logger.LogWarning("[Performance] Request of type {RequestType} took {ElapsedMilliseconds} ms to complete", typeof(TRequest).Name, timeTaken.TotalMilliseconds);
        }
        timer.Stop();
        logger.LogInformation("[END] Handled request of type {RequestType} with response of type {ResponseType}", typeof(TRequest).Name, typeof(TResponse).Name);

        return response;
    }
}
