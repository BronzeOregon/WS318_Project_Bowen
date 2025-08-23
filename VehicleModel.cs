using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class VehicleModel
{
    public int Id { get; set; }

    public int M { get; set; }

    public int Bs { get; set; }

    public int FrontAv { get; set; }

    public int SideAv { get; set; }

    public int RearAv { get; set; }

    public int Hp { get; set; }

    public int? Capacity { get; set; }

    public string Type { get; set; } = null!;

    public string Traits { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<UnitVehicleModel> UnitVehicleModels { get; set; } = new List<UnitVehicleModel>();

    public virtual ICollection<VehicleModelRangedWeapon> VehicleModelRangedWeapons { get; set; } = new List<VehicleModelRangedWeapon>();

    public virtual ICollection<VehicleModelSpecialRule> VehicleModelSpecialRules { get; set; } = new List<VehicleModelSpecialRule>();

    public bool Equals(VehicleModel obj)
    {
        if (obj == null) return false;
        else if (this.Id == obj.Id) return true;
        else return false;
    }
}
