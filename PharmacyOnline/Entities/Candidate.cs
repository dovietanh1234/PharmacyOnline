using System;
using System.Collections.Generic;

namespace PharmacyOnline.Entities;

public partial class Candidate
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Role { get; set; }

    public string? Thumbnail { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? QuantityProfile { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool? IsAtive { get; set; }

    public bool? IsUse { get; set; }

    public virtual ICollection<KeyToken> KeyTokens { get; set; } = new List<KeyToken>();
}
