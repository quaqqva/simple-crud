using Backend.Domain.Common;

namespace Backend.Domain.Entities;

public record Order : BaseEntity
{
    public required int ProductQuantity { get; set; }

    public required Guid ProductId { get; set; }

    public required Guid ContractId { get; set; }

    public virtual Contract? Contract { get; init; }

    public virtual Product? Product { get; init; }
}
