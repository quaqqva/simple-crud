namespace backend.Entities;

public record Customer : IIdentifiable
{
    public int? Id { get; set; }

    public required string Name { get; set; }

    public required int AddressId { get; set; }

    public virtual Address? Address { get; init; }

    public virtual ICollection<Contract>? Contracts { get; init; }
}
