namespace backend.Models;

public partial class Product
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public int WorkshopNumber { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Workshop WorkshopNumberNavigation { get; set; } = null!;
}
