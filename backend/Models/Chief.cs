namespace backend.Models;

public partial class Chief
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public virtual ICollection<Workshop> Workshops { get; } = new List<Workshop>();
}
