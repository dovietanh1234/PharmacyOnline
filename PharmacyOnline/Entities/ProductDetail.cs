using System;
using System.Collections.Generic;

namespace PharmacyOnline.Entities;

public partial class ProductDetail
{
    public int Id { get; set; }

    public string? ModelNumber { get; set; }

    public string? Dies { get; set; }

    public string? MaxPressure { get; set; }

    public string? MaxDiameterOfTablet { get; set; }

    public string? MaxDepthOfFill { get; set; }

    public string? ProductionCapacity { get; set; }

    public string? MachineSize { get; set; }

    public string? NetWeight { get; set; }

    public string? Output { get; set; }

    public string? CapsuleSize { get; set; }

    public string? MachineDimension { get; set; }

    public string? ShippingWeight { get; set; }

    public string? AirPressure { get; set; }

    public string? AirVolume { get; set; }

    public string? FillingSpeed { get; set; }

    public string? FillingRange { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
