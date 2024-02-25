using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record ContractDto
{
    [Required(ErrorMessage = "Contract's completion date must be specified")]
    public required DateOnly CompletionDate { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    [Required(ErrorMessage = "Contract's customer ID must be specified")]
    public required Guid CustomerId { get; set; }
}
