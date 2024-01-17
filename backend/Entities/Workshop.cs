namespace backend.Entities;

public class Workshop: IIdentifiable
{
    public int? Id { get; set; }

    public required string Name { get; set; }

    public required string PhoneNumber { get; set; }

    public required int ChiefId { get; set; }

    public virtual Chief? Chief { get; set; }

    public virtual ICollection<Product>? Products { get; }
}
