namespace backend.Entities;

public class Order: IIdentifiable
{
    public int? Id { get; set; }

    public required int ProductQuantity { get; set; }

    public required int ProductCode { get; set; }

    public required int ContractNumber { get; set; }

    public virtual Contract? Contract { get; init; }

    public virtual Product? Product { get; init; }
}
