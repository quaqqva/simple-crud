﻿using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public partial class WorkshopViewModel {
    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int ChiefId { get; set; }
}

public partial class Workshop: WorkshopViewModel
{
    public int? Number { get; set; }

    public virtual Chief Chief { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
