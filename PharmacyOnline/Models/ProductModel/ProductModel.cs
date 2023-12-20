using System.ComponentModel.DataAnnotations;

namespace PharmacyOnline.Models.ProductModel
{
    public class ProductTablet
    {
        [Required]
        public int cateId { get; set; }
        [Required]
        public string productName { get; set; }

        public string title { get; set; } = string.Empty;

        public IFormFile thumbnail { get; set; }

        public string modelNumber { get; set; } = string.Empty;
        public string dies { get; set; } = string.Empty;

        public string maxPressure { get; set; } = string.Empty;
        public string maxDiameterOfTablet { get; set; } = string.Empty;

        public string maxDepthOfFill { get; set; } = string.Empty;
        public string productionCapacity { get; set; } = string.Empty;

        public string machineSize { get; set; } = string.Empty;

        public string netWeight { get; set; } = string.Empty;
    }


    public class ProductCapsule
    {
        [Required]
        public int cateId { get; set; }
        [Required]
        public string productName { get; set; }

        public string title { get; set; } = string.Empty;

        public IFormFile thumbnail { get; set; }

        public string? Output { get; set; }

        public string? CapsuleSize { get; set; }

        public string? MachineDimension { get; set; }

        public string? ShippingWeight { get; set; }
    }



    public class ProductLiquid
    {
        [Required]
        public int cateId { get; set; }
        [Required]
        public string productName { get; set; }

        public string title { get; set; } = string.Empty;

        public IFormFile thumbnail { get; set; }

        public string? AirPressure { get; set; }

        public string? AirVolume { get; set; }

        public string? FillingSpeed { get; set; }

        public string? FillingRange { get; set; }
    }


    public class ProductTablet2
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string? productName { get; set; } = "";

        public string? title { get; set; } = "";

        public IFormFile? thumbnail { get; set; } = null;

        public string? modelNumber { get; set; } = "";
        public string? dies { get; set; } = "";

        public string? maxPressure { get; set; } = "";
        public string? maxDiameterOfTablet { get; set; } = "";

        public string? maxDepthOfFill { get; set; } = "";
        public string? productionCapacity { get; set; } = "";

        public string? machineSize { get; set; } = "";

        public string? netWeight { get; set; } = "";
    }


    public class ProductCapsule2
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string? productName { get; set; } = "";

        public string? title { get; set; } = "";

        public IFormFile? thumbnail { get; set; } = null;

        public string? Output { get; set; } = "";

        public string? CapsuleSize { get; set; } = "";

        public string? MachineDimension { get; set; } = "";

        public string? ShippingWeight { get; set; } = "";
    }

    public class ProductLiquid2
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string? productName { get; set; } = "";

        public string? title { get; set; } = "";

        public IFormFile? thumbnail { get; set; } = null;

        public string? AirPressure { get; set; } = "";

        public string? AirVolume { get; set; } = "";

        public string? FillingSpeed { get; set; } = "";

        public string? FillingRange { get; set; } = "";
    }


}
