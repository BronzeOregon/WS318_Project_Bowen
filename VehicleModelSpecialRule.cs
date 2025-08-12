using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class VehicleModelSpecialRule
{
    public int VehicleModelId { get; set; }

    public int SpecialRulesId { get; set; }

    public int Id { get; set; }

    public virtual SpecialRule SpecialRules { get; set; } = null!;

    public virtual VehicleModel VehicleModel { get; set; } = null!;
}
