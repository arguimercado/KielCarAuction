namespace BuildingBlocks.Messaging.Contracts;

public record IntegrationEvent
{
    public Guid Id => Guid.NewGuid();

    public DateTime OccuredOn => DateTime.Now;

    public string EventType => GetType().AssemblyQualifiedName ?? "";
}
