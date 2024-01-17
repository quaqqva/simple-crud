using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class CustomerDTO {
    [Required]
    [MaxLength(45)]
    public required string Name { get; set; }

    [Required]
    public required int AddressId { get; set; }
}