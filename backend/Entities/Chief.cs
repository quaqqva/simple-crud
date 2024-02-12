namespace backend.Entities;

public record Chief : IIdentifiable
{
    public int? Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? Patronymic { get; set; }

    public virtual ICollection<Workshop>? Workshops { get; init; }
}
