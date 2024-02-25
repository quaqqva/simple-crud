using System.ComponentModel;

namespace backend.Entities;

public record Product : IIdentifiable
{
    public Guid? Id { get; set; }

    public required string Name { get; set; }

    [DefaultValue(-1)]
    public required int Price { get; set; } = -1;

    public required Guid WorkshopId { get; set; }

    public virtual ICollection<Order>? Orders { get; init; }

    public virtual Workshop? Workshop { get; init; }
}
