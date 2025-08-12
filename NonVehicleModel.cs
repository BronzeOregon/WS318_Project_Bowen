using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class NonVehicleModel
{
    public int Id { get; set; }

    public int M { get; set; }

    public int Ws { get; set; }

    public int Bs { get; set; }

    public int S { get; set; }

    public int T { get; set; }

    public int W { get; set; }

    public int I { get; set; }

    public int A { get; set; }

    public int Ld { get; set; }

    public int Cl { get; set; }

    public int Wp { get; set; }

    public int Int { get; set; }

    public string Sv { get; set; } = null!;

    public string Inv { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Traits { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<NonVehicleModelMeleeWeapon> NonVehicleModelMeleeWeaponMeleeWeaponIdtoModelNavigations { get; set; } = new List<NonVehicleModelMeleeWeapon>();

    public virtual ICollection<NonVehicleModelMeleeWeapon> NonVehicleModelMeleeWeaponNonVehicleModels { get; set; } = new List<NonVehicleModelMeleeWeapon>();

    public virtual ICollection<NonVehicleModelRangedWeapon> NonVehicleModelRangedWeapons { get; set; } = new List<NonVehicleModelRangedWeapon>();

    public virtual ICollection<NonVehicleModelSpecialRule> NonVehicleModelSpecialRules { get; set; } = new List<NonVehicleModelSpecialRule>();

    public virtual ICollection<UnitNonVehicleModel> UnitNonVehicleModels { get; set; } = new List<UnitNonVehicleModel>();
}
