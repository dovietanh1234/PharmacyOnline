namespace PharmacyOnline.DTO.ProfileDTO
{
    public class ProfileDTO
    {
        public string Id { get; set; }
        public string fullname { get; set; }

        public string Email { get; set; }

        public string? Number { get; set; } = "";
        public string University { get; set; }

        public string status { get; set; }

        public string isAccepted { get; set; }


    }
}
