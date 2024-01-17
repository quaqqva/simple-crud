using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class AddressDTO {
    [Required]
    [MaxLength(45)]
    public required string Country { get; set; }

    [Required]
    [MaxLength(45)]
    public required string City { get; set; }

    [Required]
    [MaxLength(45)]
    public required string Street { get; set; }
    
    [Required]
    [MaxLength(45)]
    public required string Building { get; set; }
}