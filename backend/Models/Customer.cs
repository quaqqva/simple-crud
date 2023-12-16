namespace backend.Models;

public partial class CustomerViewModel {
    public string Name { get; set; } = null!;

    public int AddressId { get; set; }
}

public partial class Customer: CustomerViewModel
{
    public int? Id { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();
}
