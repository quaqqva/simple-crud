using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class WorkshopDTO {
    [Required]
    [MaxLength(90)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(20)]
    public required string PhoneNumber { get; set; }

    [Required]
    public required int ChiefId { get; set; }
}