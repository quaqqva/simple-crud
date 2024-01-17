using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class ContractDTO {
    [Required(ErrorMessage = "Contract's completion date must be specified")]
    public required DateOnly CompletionDate { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    [Required(ErrorMessage = "Contract's customer ID must be specified")]
    public required int CustomerId { get; set; }
}