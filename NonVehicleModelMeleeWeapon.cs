using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class NonVehicleModelMeleeWeapon
{
    public int NonVehicleModelId { get; set; }

    public int MeleeWeaponIdtoModel { get; set; }

    public int Id { get; set; }

    public virtual NonVehicleModel MeleeWeaponIdtoModelNavigation { get; set; } = null!;

    public virtual NonVehicleModel NonVehicleModel { get; set; } = null!;
}
