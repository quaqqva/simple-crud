namespace backend.Entities;

public record Product : IIdentifiable
{
    public int? Id { get; set; }

    public required string Name { get; set; }

    public required int Price { get; set; }

    public required int WorkshopId { get; set; }

    public virtual ICollection<Order>? Orders { get; init; }

    public virtual Workshop? Workshop { get; init; }
}
