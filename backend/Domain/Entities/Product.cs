using System.ComponentModel;
using Backend.Domain.Common;

namespace Backend.Domain.Entities;

public record Product : BaseEntity
{
    public required string Name { get; set; }

    [DefaultValue(-1)] public required int Price { get; set; } = -1;

    public required Guid WorkshopId { get; set; }

    public virtual ICollection<Order>? Orders { get; init; }

    public virtual Workshop? Workshop { get; init; }
}