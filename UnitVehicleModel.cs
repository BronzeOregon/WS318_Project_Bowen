using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class UnitVehicleModel
{
    public int UnitId { get; set; }

    public int VehicleModelId { get; set; }

    public int Id { get; set; }

    public virtual Unit Unit { get; set; } = null!;

    public virtual VehicleModel VehicleModel { get; set; } = null!;
}
