namespace backend.Models;

public partial class ProductViewModel {
    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public int WorkshopNumber { get; set; }
}

public partial class Product: ProductViewModel
{
    public int? Code { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Workshop Workshop { get; set; } = null!;
}
