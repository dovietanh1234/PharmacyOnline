namespace PharmacyOnline.DTO.ManageCandidatesDTO
{

    public class ListCans
    {
        public int id { get; set; }
        public string? username { get; set; } = "";
        public string? role { get; set; } = "";

        public string? thumbnail { get; set; } = "";

        public string? email { get; set; } = "";

        public bool? isActive { get; set; }
    }

    public class ListCans2
    {
        public int id { get; set; }
        public string? username { get; set; } = "";
        public string? role { get; set; } = "";

        public IFormFile? thumbnail { get; set; } = null;

        public string? email { get; set; } = "";

        public bool? isActive { get; set; }
    }


    public class updateCan
    {
        public int id { get; set; }
        public string? username { get; set; } = "";

        public IFormFile? thumbnail { get; set; } = null;

    }



}
