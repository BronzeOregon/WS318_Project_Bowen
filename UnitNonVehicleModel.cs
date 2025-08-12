using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class UnitNonVehicleModel
{
    public int UnitId { get; set; }

    public int NonVehicleModelId { get; set; }

    public int Id { get; set; }

    public virtual NonVehicleModel NonVehicleModel { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
