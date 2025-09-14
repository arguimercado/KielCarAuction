namespace DomainBlocks.Contracts;

public interface IUnitWork
{
    Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);
}
