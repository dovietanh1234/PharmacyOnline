namespace PharmacyOnline.DTO.ProductDTO
{
    public class ProductTabletDTO
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public string title { get; set; }
        public string thumbnail { get; set; }
        public int cateId { get; set; }
        public int productDetailId { get; set; }

        public virtual CategoryGet cateGet { get; set; }

        public virtual Tablet TabletP { get; set; }

    }

    public class Tablet
    {
        public string modelNumber { get; set; } = string.Empty;
        public string dies { get; set; } = string.Empty;
        public string maxPressure { get; set; } = string.Empty;
        public string maxDiameterOfTablet { get; set; } = string.Empty;
        public string maxDepthOfFill { get; set; } = string.Empty;
        public string productionCapacity { get; set; } = string.Empty;
        public string machineSize { get; set; } = string.Empty;
        public string netWeight { get; set; } = string.Empty;
    }

    public class capsule
    {
        public string? Output { get; set; }

        public string? CapsuleSize { get; set; }

        public string? MachineDimension { get; set; }

        public string? ShippingWeight { get; set; }
    }

    public class liquid
    {
        public string? AirPressure { get; set; }

        public string? AirVolume { get; set; }

        public string? FillingSpeed { get; set; }

        public string? FillingRange { get; set; }
    }

    public class CategoryGet{
            public string CateName { get; set; } = string.Empty;
    }


    public class ProductCapsuleDTO
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public string title { get; set; }
        public string thumbnail { get; set; }
        public int cateId { get; set; }
        public int productDetailId { get; set; }

        public virtual CategoryGet cateGet { get; set; }

        public virtual capsule CapsuleP { get; set; }

    }


    public class ProductLiquidDTO
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public string title { get; set; }
        public string thumbnail { get; set; }
        public int cateId { get; set; }
        public int productDetailId { get; set; }

        public virtual CategoryGet cateGet { get; set; }

        public virtual liquid LiquidP { get; set; }

    }

    public class getAll
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public string title { get; set; }
        public string thumbnail { get; set; }
        public virtual CategoryGet cateGet { get; set; }
    }


}
