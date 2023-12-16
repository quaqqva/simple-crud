namespace backend.Models;

public partial class Totalprice
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public decimal? TotalPrice1 { get; set; }
}
