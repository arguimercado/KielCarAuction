using BuildingBlocks.Commons.Errors;
using Carter;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BuildingBlocks.Commons.Abstracts;

public abstract class BaseModule : ICarterModule
{
    public IResult HandleResults<T>(Result<T> data)
    {
        if (data.IsFailed)
        {
            // Check if any error is a ValidationError
            bool hasNotFoundError = data.Errors.Any(e => e is NotFoundErrorResult);
            if (hasNotFoundError)
            {
                // Handle validation error (e.g., return BadRequest)
                return TypedResults.NotFound(data.Errors);
            }
            else
            {
                return TypedResults.BadRequest(data.Errors);
            }
            
            // Handle other errors as needed
        }

        return TypedResults.Ok(data.Value);
    }
    public virtual void AddRoutes(IEndpointRouteBuilder app)
    {
    }
}
