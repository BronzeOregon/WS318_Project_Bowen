using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class SpecialRule
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Detail { get; set; }

    public virtual ICollection<MeleeWeaponSpecialRule> MeleeWeaponSpecialRules { get; set; } = new List<MeleeWeaponSpecialRule>();

    public virtual ICollection<NonVehicleModelSpecialRule> NonVehicleModelSpecialRules { get; set; } = new List<NonVehicleModelSpecialRule>();

    public virtual ICollection<RangedWeaponSpecialRule> RangedWeaponSpecialRules { get; set; } = new List<RangedWeaponSpecialRule>();

    public virtual ICollection<VehicleModelSpecialRule> VehicleModelSpecialRules { get; set; } = new List<VehicleModelSpecialRule>();

    public bool Equals(SpecialRule obj)
    {
        if (obj == null) return false;
        else if (this.Id == obj.Id) return true;
        else return false;
    }
}
