namespace backend.Entities;

public record Workshop : IIdentifiable
{
    public Guid? Id { get; set; }

    public required string Name { get; set; }

    public required string PhoneNumber { get; set; }

    public required Guid ChiefId { get; set; }

    public virtual Chief? Chief { get; init; }

    public virtual ICollection<Product>? Products { get; init; }
}
