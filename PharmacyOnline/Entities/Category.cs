using System;
using System.Collections.Generic;

namespace PharmacyOnline.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string? CateName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
