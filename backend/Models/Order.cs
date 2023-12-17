namespace backend.Models;

public partial class OrderViewModel {
    public int ProductQuantity { get; set; }

    public int ProductCode { get; set; }

    public int ContractNumber { get; set; }
}

public partial class Order: OrderViewModel
{
    public int? Id { get; set; }

    public virtual Contract Contract { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
