using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class OrderDTO {
    [Required]
    public required int ProductQuantity { get; set; }

    [Required]
    public required int ProductCode { get; set; }

    [Required]
    public required int ContractNumber { get; set; }
}