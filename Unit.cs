using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class Unit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ModelCount { get; set; }

    public virtual ICollection<UnitNonVehicleModel> UnitNonVehicleModels { get; set; } = new List<UnitNonVehicleModel>();

    public virtual ICollection<UnitVehicleModel> UnitVehicleModels { get; set; } = new List<UnitVehicleModel>();
}
