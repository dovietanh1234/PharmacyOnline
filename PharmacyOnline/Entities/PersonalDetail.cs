using System;
using System.Collections.Generic;

namespace PharmacyOnline.Entities;

public partial class PersonalDetail
{
    public string Id { get; set; } = null!;

    public int CandidateId { get; set; }

    public string? Fullname { get; set; }

    public string? Address { get; set; }

    public string? Number { get; set; }

    public string Email { get; set; } = null!;

    public string? Thumbnail { get; set; }

    public string? FileCv { get; set; }

    public string? Skills { get; set; }

    public string? UniversityOrCollege { get; set; }

    public string? Major { get; set; }

    public DateTime? IssuedDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? ScientificAchievements { get; set; }

    public string? WorkExperiences { get; set; }

    public string? Reference { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? IsAccepted { get; set; }

    public string? Age { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;
}
