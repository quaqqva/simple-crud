using Backend.Domain.Common;

namespace Backend.Domain.Entities;

public record Address : BaseEntity
{
    public required string Country { get; set; }

    public required string City { get; set; }

    public required string Street { get; set; }

    public required string Building { get; set; }

    public virtual ICollection<Customer>? Customers { get; init; }
}