using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record OrderDto
{
    [Required(ErrorMessage = "Order's product quantity must be specified")]
    public required int ProductQuantity { get; set; }

    [Required(ErrorMessage = "Order's product code must be specified")]
    public required int ProductCode { get; set; }

    [Required(ErrorMessage = "Order's contract number must be specified")]
    public required int ContractNumber { get; set; }
}
