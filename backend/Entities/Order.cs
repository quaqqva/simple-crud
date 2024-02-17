namespace backend.Entities;

public record Order : IIdentifiable
{
    public int? Id { get; set; }

    public required int ProductQuantity { get; set; }

    public required int ProductId { get; set; }

    public required int ContractId { get; set; }

    public virtual Contract? Contract { get; init; }

    public virtual Product? Product { get; init; }
}
