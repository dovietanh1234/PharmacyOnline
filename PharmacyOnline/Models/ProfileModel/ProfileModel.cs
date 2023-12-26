using System.ComponentModel.DataAnnotations;

namespace PharmacyOnline.Models.ProfileModel
{
    public class ProfileModel
    {
        [Required]
        public int IdCandidate { get; set; }
        [Required]
        public string? Fullname { get; set; }
        public string? Address { get; set; } = "";
        [Required]
        public string? Number { get; set; } = "";
        public string Email { get; set; } = "";
        public IFormFile? Thumbnail { get; set; } = null;
        public IFormFile? FileCv { get; set; } = null;
        public string? Skills { get; set; } = "";

        public string? UniversityOrCollege { get; set; } = "";
        public string? Major { get; set; } = "";
        public DateTime? IssuedDate { get; set; } = null;
        public DateTime? ExpiryDate { get; set; } = null;
        public string? ScientificAchievements { get; set; } = "";

        public string? WorkExperiences { get; set; } = "";
        public string? Reference { get; set; } = "";

    }
}
