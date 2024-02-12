using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class AddressDTO
{
    [Required(ErrorMessage = "Address' country is required")]
    [MaxLength(45)]
    public required string Country { get; set; }

    [Required(ErrorMessage = "Address' city is required")]
    [MaxLength(45)]
    public required string City { get; set; }

    [Required(ErrorMessage = "Address' street is required")]
    [MaxLength(45)]
    public required string Street { get; set; }

    [Required(ErrorMessage = "Address' building is required")]
    [MaxLength(45)]
    public required string Building { get; set; }
}
