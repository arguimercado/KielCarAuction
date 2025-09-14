using FluentResults;
using MediatR;

namespace BuildingBlocks.CQRS;

// ICommand does not require variance, so we remove the 'in' keyword from TResponse
public interface ICommand : IRequest<Result<Unit>> { }

// Fix: Remove the 'out' keyword from TResponse to make it invariant
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    where TResponse : notnull
{
}

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result<Unit>>
    where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
}