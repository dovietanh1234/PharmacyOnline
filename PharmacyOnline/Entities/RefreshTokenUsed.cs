using System;
using System.Collections.Generic;

namespace PharmacyOnline.Entities;

public partial class RefreshTokenUsed
{
    public int Id { get; set; }

    public int IdKeyToken { get; set; }

    public string? RefreshTokenUsed1 { get; set; }

    public virtual KeyToken IdKeyTokenNavigation { get; set; } = null!;
}
