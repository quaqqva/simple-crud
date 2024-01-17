namespace backend.DTOs;

public class ContractDTO {
    public DateOnly? CompletionDate { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public int? CustomerId { get; set; }
}