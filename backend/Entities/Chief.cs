using System.Text.Json.Serialization;

namespace backend.Entities;

public class Chief: IIdentifiable
{
    public int? Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string? Patronymic { get; set; }

    public virtual ICollection<Workshop>? Workshops { get; }
}
