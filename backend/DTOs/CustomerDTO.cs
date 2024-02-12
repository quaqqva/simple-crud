using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class CustomerDTO
{
    [Required(ErrorMessage = "Customer's name must be specified")]
    [MaxLength(45)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Customer's address' ID must be specified")]
    public required int AddressId { get; set; }
}
