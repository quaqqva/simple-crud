using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities;

public record Contract : IIdentifiable
{
    public Guid? Id { get; set; }

    [DefaultValue("0001-01-01")]
    public required DateOnly CompletionDate { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public required Guid CustomerId { get; set; }

    public virtual Customer? Customer { get; init; }

    public virtual ICollection<Order>? Orders { get; init; }
}
