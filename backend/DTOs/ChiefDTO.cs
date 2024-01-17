using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class ChiefDTO {
    [Required]
    [MaxLength(45)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(45)]
    public required string LastName { get; set; }

    [MaxLength(45)]
    public string? Patronymic { get; set; }
}