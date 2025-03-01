﻿using System;
using System.Collections.Generic;

namespace U2D_RPG_Demo.ApiServer.Models;

public partial class PlayerAttributes
{
    public int PAID { get; set; }

    public string? FormattedPAID { get; set; }

    public int UID { get; set; }

    public int? Level { get; set; }

    public int? Experience { get; set; }

    public int? Hp { get; set; }

    public int? MaxHp { get; set; }

    public int? Mp { get; set; }

    public int? MaxMp { get; set; }

    public int? ATK { get; set; }

    public int? DEF { get; set; }

    public double? DR { get; set; }

    public int? SPD { get; set; }

    public double? SPDMult { get; set; }

    public virtual UserInfo UidNavigation { get; set; } = null!;
}
