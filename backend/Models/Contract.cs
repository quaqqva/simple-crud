namespace backend.Models;

public partial class ContractViewModel {
    public DateOnly CompletionDate { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public int CustomerId { get; set; }
}

public partial class Contract: ContractViewModel
{
    public int? Number { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
