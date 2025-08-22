using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class RangedWeapon
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Range { get; set; }

    public int Fp { get; set; }

    public int Rs { get; set; }

    public int Ap { get; set; }

    public int D { get; set; }

    public string? Traits { get; set; }

    public virtual ICollection<NonVehicleModelRangedWeapon> NonVehicleModelRangedWeapons { get; set; } = new List<NonVehicleModelRangedWeapon>();

    public virtual ICollection<RangedWeaponSpecialRule> RangedWeaponSpecialRules { get; set; } = new List<RangedWeaponSpecialRule>();

    public virtual ICollection<VehicleModelRangedWeapon> VehicleModelRangedWeapons { get; set; } = new List<VehicleModelRangedWeapon>();

    public bool Equals(RangedWeapon obj)
    {
        if (obj == null) return false;
        else if (this.Id == obj.Id) return true;
        else return false;
    }
}
