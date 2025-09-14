using BuildingBlocks.Commons.Errors;
using FluentResults;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Commons.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase // Added this constraint to satisfy the 'notnull' requirement
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context, cancellationToken))
        );

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(result => result.Errors)
            .ToList();

        if (failures.Any())
        {
            // Add result pattern
            var errorMessages = failures.Select(f => f.ErrorMessage).ToList();
            var errorMessage = string.Join(", ", errorMessages);
            var error = new ValidationErrorResult(errorMessage, failures.Select(f => f.PropertyName).ToList());

            // If TResponse is a generic Result<T>, create a failed Result<T> with default value
            var tResponseType = typeof(TResponse);
            if (tResponseType.IsGenericType && tResponseType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var genericArg = tResponseType.GetGenericArguments()[0];
                var failMethod = typeof(Result)
                    .GetMethods()
                    .First(m => m.Name == "Fail" && m.IsGenericMethod && m.GetParameters().Length == 1);

                var genericFailMethod = failMethod.MakeGenericMethod(genericArg);
                var result = genericFailMethod.Invoke(null, new object[] { error });
                return (TResponse)result;
            }

            // Otherwise, return non-generic Result with error
            return (TResponse)(object)Result.Fail(error);

        }

        return await next();
    }
}
