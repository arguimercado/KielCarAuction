using DomainBlocks.Domains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DomainBlocks.Interceptors;

public class DomainEventInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context)
            .GetAwaiter()
            .GetResult();
        
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    /// <summary>
    /// Dispatches domain events after saving changes to the database.
    /// </summary>
    /// <param name="eventData"></param>
    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = aggregates
            .Select(a => a.GetDomainEvents())
            .SelectMany(a => a)
            .ToList();
      
        foreach (var domainEvent in domainEvents) {
           await mediator.Publish(domainEvent);
        }

    }


}
