namespace DomainBlocks.Domains;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
{


    private readonly List<IDomainEvent> _domainEvents = new();


    protected AggregateRoot(TId id) : base(id)
    {
       
    }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent == null) throw new ArgumentNullException(nameof(domainEvent));
        _domainEvents.Add(domainEvent);
    }

    public IDomainEvent[] GetDomainEvents()
    {
        IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        
        return dequeuedEvents;
    }
}
