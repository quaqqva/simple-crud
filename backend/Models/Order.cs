namespace backend.Models;


public class Order: IIdentifiable
{
    public int? Id { get; set; }

    public required int ProductQuantity { get; set; }

    public required int ProductCode { get; set; }

    public required int ContractNumber { get; set; }

    public virtual Contract? Contract { get; set; }

    public virtual Product? Product { get; set; }
}
