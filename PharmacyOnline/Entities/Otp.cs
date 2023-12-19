using System;
using System.Collections.Generic;

namespace PharmacyOnline.Entities;

public partial class Otp
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string OtpHash { get; set; } = null!;

    public int? OtpSpamNumber { get; set; }

    public DateTime? OtpSpam { get; set; }

    public DateTime? LimitTimeToSendOtp { get; set; }
}
