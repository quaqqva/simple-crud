using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record ChiefDto
{
    [Required(ErrorMessage = "Chief's first name is required")]
    [MaxLength(45)]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Chief's last name is required")]
    [MaxLength(45)]
    public required string LastName { get; set; }

    [MaxLength(45)]
    public string? Patronymic { get; set; }
}
