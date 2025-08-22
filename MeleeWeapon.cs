using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class MeleeWeapon
{
    public int Id { get; set; }

    public string Im { get; set; } = null!;

    public string Am { get; set; } = null!;

    public string As { get; set; } = null!;

    public int Ap { get; set; }

    public int D { get; set; }

    public string? Traits { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MeleeWeaponSpecialRule> MeleeWeaponSpecialRules { get; set; } = new List<MeleeWeaponSpecialRule>();

    public bool Equals(MeleeWeapon obj)
    {
        if (obj == null) return false;
        else if (this.Id == obj.Id) return true;
        else return false;
    }
}
