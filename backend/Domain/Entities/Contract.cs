using System.ComponentModel;
using Backend.Domain.Common;
namespace Backend.Domain.Entities;

public record Contract : BaseEntity
{
    [DefaultValue("0001-01-01")]
    public required DateOnly CompletionDate { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public required Guid CustomerId { get; set; }

    public virtual Customer? Customer { get; init; }

    public virtual ICollection<Order>? Orders { get; init; }
}
