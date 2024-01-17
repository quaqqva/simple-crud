using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class ContractDTO {
    [Required]
    public required DateOnly CompletionDate { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    [Required]
    public required int CustomerId { get; set; }
}