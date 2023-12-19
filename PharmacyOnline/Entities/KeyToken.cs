using System;
using System.Collections.Generic;

namespace PharmacyOnline.Entities;

public partial class KeyToken
{
    public int Id { get; set; }

    public int IdCandidate { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? createAt { get; set; }

    public virtual Candidate IdCandidateNavigation { get; set; } = null!;

    public virtual ICollection<RefreshTokenUsed> RefreshTokenUseds { get; set; } = new List<RefreshTokenUsed>();
}
