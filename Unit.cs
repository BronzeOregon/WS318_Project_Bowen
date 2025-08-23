using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class Unit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ModelCount { get; set; }

    public virtual ICollection<UnitNonVehicleModel> UnitNonVehicleModels { get; set; } = null!;

    public virtual ICollection<UnitVehicleModel> UnitVehicleModels { get; set; } = null!;

    public bool Equals(Unit obj)
    {
        if (obj == null) return false;
        else if (this.Id == obj.Id) return true;
        else return false;
    }

    override public string ToString()
    {
        return this.Name;
    }
}
