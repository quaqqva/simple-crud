using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class ProductDTO {
    [Required]
    [MaxLength(45)]
    public required string Name { get; set; }

    [Required]
    public required int Price { get; set; }

    [Required]
    public required int WorkshopNumber { get; set; }
}