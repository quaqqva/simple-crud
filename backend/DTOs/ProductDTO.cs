using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public record ProductDTO
{
    [Required(ErrorMessage = "Product's name is required")]
    [MaxLength(45)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Product's price is required")]
    public required int Price { get; set; }

    [Required(ErrorMessage = "Product's workshop number is required")]
    public required int WorkshopNumber { get; set; }
}
