namespace backend.Entities;

public class Product: IIdentifiable
{
    public int? Id { get; set; }

    public required string Name { get; set; }

    public required int Price { get; set; }

    public required int WorkshopNumber { get; set; }
    
    public virtual ICollection<Order>? Orders { get; }

    public virtual Workshop? Workshop { get; set; }
}
