using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Dtos.PlayerAttribute
{
    public class PlayerAttributeDto
    {
        public int Paid { get; set; }

        public int Uid { get; set; }

        public int? Level { get; set; }

        public int? Experience { get; set; }

        public double? Hp { get; set; }

        public double? MaxHp { get; set; }

        public double? Mp { get; set; }

        public double? MaxMp { get; set; }

        public double? Atk { get; set; }

        public double? Def { get; set; }

        public double? Dr { get; set; }

        public double? Spd { get; set; }

        public double? SpdMult { get; set; }
    }
}
