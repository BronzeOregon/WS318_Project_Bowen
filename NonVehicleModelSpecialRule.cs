using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class NonVehicleModelSpecialRule
{
    public int NonVehicleModelId { get; set; }

    public int SpecialRulesId { get; set; }

    public int Id { get; set; }

    public virtual NonVehicleModel NonVehicleModel { get; set; } = null!;

    public virtual SpecialRule SpecialRules { get; set; } = null!;
}
