namespace backend.Models;

public partial class AddressViewModel {
    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Building { get; set; } = null!;
}

public partial class Address: AddressViewModel
{
    public int? Id { get; set; }
    
    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();

}
