using U2D_RPG_Demo.ApiServer.Dtos.PlayerAttribute;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Mappers
{
    public static class PlayerAttributeMappers {
        public static PlayerAttributeDto ToPlayerAttributeDto(this PlayerAttribute playerAttributeModel) {
            return new PlayerAttributeDto {
                Paid = playerAttributeModel.Paid,
                Uid = playerAttributeModel.Uid,
                Level = playerAttributeModel.Level,
                Experience = playerAttributeModel.Experience,
                Hp = playerAttributeModel.Hp,
                MaxHp = playerAttributeModel.MaxHp,
                Mp = playerAttributeModel.Mp,
                MaxMp = playerAttributeModel.MaxMp,
                Atk = playerAttributeModel.Atk,
                Def = playerAttributeModel.Def,
                Dr = playerAttributeModel.Dr,
                Spd = playerAttributeModel.Spd,
                SpdMult = playerAttributeModel.SpdMult,
            };
        }
    }
}
