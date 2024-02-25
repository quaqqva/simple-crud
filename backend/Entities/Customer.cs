namespace backend.Entities;

public record Customer : IIdentifiable
{
    public Guid? Id { get; set; }

    public required string Name { get; set; }

    public required Guid AddressId { get; set; }

    public virtual Address? Address { get; init; }

    public virtual ICollection<Contract>? Contracts { get; init; }
}
