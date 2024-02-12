using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record WorkshopDto
{
    [Required]
    [MaxLength(90)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(20)]
    [Phone]
    public required string PhoneNumber { get; set; }

    [Required]
    public required int ChiefId { get; set; }
}
