using Backend.Domain.Common;

namespace Backend.Domain.Entities;

public record Chief : BaseEntity
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? Patronymic { get; set; }

    public virtual ICollection<Workshop>? Workshops { get; init; }
}
