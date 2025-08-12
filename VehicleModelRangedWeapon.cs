using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class VehicleModelRangedWeapon
{
    public int VehicleModelId { get; set; }

    public int RangedWeaponId { get; set; }

    public int Id { get; set; }

    public virtual RangedWeapon RangedWeapon { get; set; } = null!;

    public virtual VehicleModel VehicleModel { get; set; } = null!;
}
