using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Dtos;

public record OrderDto
{
    [Required(ErrorMessage = "Order's product quantity must be specified")]
    public required int ProductQuantity { get; set; }

    [Required(ErrorMessage = "Order's product id must be specified")]
    public required Guid ProductId { get; set; }

    [Required(ErrorMessage = "Order's contract id must be specified")]
    public required Guid ContractId { get; set; }
}
