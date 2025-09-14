namespace DomainBlocks.Domains;

public abstract class Entity<T> : IEntity<T>
{
    protected Entity() 
    {
    }
    protected Entity(T value) {
        Id = value;
        CreatedOn = DateTime.Now;
        LastModifiedOn = DateTime.Now;
    }

    public  T Id { get; set; } = default!;
    public DateTime? CreatedOn { get;set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string? LastModifiedBy { get; set; }
}
