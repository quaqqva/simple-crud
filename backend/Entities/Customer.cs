namespace backend.Entities;

public class Customer: IIdentifiable
{
    public int? Id { get; set; }

    public required string Name { get; set; }

    public required int AddressId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Contract>? Contracts { get; }
}
