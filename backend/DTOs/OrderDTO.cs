using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record OrderDto
{
    [Required(ErrorMessage = "Order's product quantity must be specified")]
    public required int ProductQuantity { get; set; }

    [Required(ErrorMessage = "Order's product id must be specified")]
    public required int ProductId { get; set; }

    [Required(ErrorMessage = "Order's contract id must be specified")]
    public required int ContractId { get; set; }
}
