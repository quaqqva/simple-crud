using Backend.Domain.Common;

namespace Backend.Domain.Entities;

public record Customer : BaseEntity
{
    public required string Name { get; set; }

    public required Guid AddressId { get; set; }

    public virtual Address? Address { get; init; }

    public virtual ICollection<Contract>? Contracts { get; init; }
}