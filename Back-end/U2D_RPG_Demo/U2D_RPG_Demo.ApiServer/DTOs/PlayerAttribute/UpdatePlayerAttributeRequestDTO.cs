﻿namespace U2D_RPG_Demo.ApiServer.DTOs.PlayerAttribute {
    public class UpdatePlayerAttributeRequestDTO {

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
    }
}