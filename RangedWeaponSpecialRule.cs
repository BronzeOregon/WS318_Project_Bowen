using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class RangedWeaponSpecialRule
{
    public int RangedWeaponId { get; set; }

    public int SpecialRuleId { get; set; }

    public int Id { get; set; }

    public virtual RangedWeapon RangedWeapon { get; set; } = null!;

    public virtual SpecialRule SpecialRule { get; set; } = null!;
}
