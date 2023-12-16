namespace backend.Models;

public partial class Contract
{
    public int Number { get; set; }

    public DateOnly CompletionDate { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
