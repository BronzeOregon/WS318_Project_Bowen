using System;
using System.Collections.Generic;

namespace WS318_Project_Bowen;

public partial class MeleeWeaponSpecialRule
{
    public int MeleeWeaponId { get; set; }

    public int SpecialRuleId { get; set; }

    public int Id { get; set; }

    public virtual MeleeWeapon MeleeWeapon { get; set; } = null!;

    public virtual SpecialRule SpecialRule { get; set; } = null!;
}
