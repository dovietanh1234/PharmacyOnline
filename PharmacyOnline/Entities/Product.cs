using System;
using System.Collections.Generic;

namespace PharmacyOnline.Entities;

public partial class Product
{
    public int Id { get; set; }

    public int CateId { get; set; }

    public int ProductDetailId { get; set; }

    public bool? IsAtive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ProductName { get; set; }

    public string? Title { get; set; }

    public string? Thumbnail { get; set; }

    public virtual Category Cate { get; set; } = null!;

    public virtual ProductDetail ProductDetail { get; set; } = null!;
}
