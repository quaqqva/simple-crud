namespace backend.Models;

public partial class ChiefViewModel {
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }
}

public partial class Chief: ChiefViewModel
{
    public int? Id { get; set; }

    public virtual ICollection<Workshop> Workshops { get; } = new List<Workshop>();
}
