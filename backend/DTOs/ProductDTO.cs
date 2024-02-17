using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record ProductDto
{
    [Required(ErrorMessage = "Product's name is required")]
    [MaxLength(45)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Product's price is required")]
    public required int Price { get; set; }

    [Required(ErrorMessage = "Product's workshop id is required")]
    public required int WorkshopId { get; set; }
}
