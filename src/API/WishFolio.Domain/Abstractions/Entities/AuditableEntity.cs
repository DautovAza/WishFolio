namespace WishFolio.Domain.Abstractions.Entities;

public abstract class AuditableEntity : IEntity
{
    protected AuditableEntity() { }

    protected AuditableEntity(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
}