using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Dtos;

public record CustomerDto
{
    [Required(ErrorMessage = "Customer's name must be specified")]
    [MaxLength(45)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Customer's address' ID must be specified")]
    public required Guid AddressId { get; set; }
}
