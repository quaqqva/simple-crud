namespace backend.Entities;

public partial class Contract : IIdentifiable
{
    public int? Id { get; set; }

    public required DateOnly CompletionDate { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public required int CustomerId { get; set; }

    public virtual Customer? Customer { get; init; }

    public virtual ICollection<Order>? Orders { get; init; }
}
