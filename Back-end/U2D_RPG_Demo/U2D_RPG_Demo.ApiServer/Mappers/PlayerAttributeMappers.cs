using U2D_RPG_Demo.ApiServer.Dtos.PlayerAttribute;
using U2D_RPG_Demo.ApiServer.Dtos.UserInfo;
using U2D_RPG_Demo.ApiServer.DTOs.PlayerAttribute;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Mappers
{
    public static class PlayerAttributeMappers {

        public static PlayerAttributeDTO ToPlayerAttributeDTO(this PlayerAttribute playerAttributeModel) {
            return new PlayerAttributeDTO {
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

        public static PlayerAttribute ToCreatePlayerAttibuteDTO(this CreatePlayerAttributeRequestDTO playerAttributeDTO, int uid) {
            return new PlayerAttribute {
                Uid = uid,
                Level = playerAttributeDTO.Level,
                Experience = playerAttributeDTO.Experience,
                Hp = playerAttributeDTO.Hp,
                MaxHp = playerAttributeDTO.MaxHp,
                Mp = playerAttributeDTO.Mp,
                MaxMp = playerAttributeDTO.MaxMp,
                Atk = playerAttributeDTO.Atk,
                Def = playerAttributeDTO.Def,
                Dr = playerAttributeDTO.Dr,
                Spd = playerAttributeDTO.Spd,
                SpdMult = playerAttributeDTO.SpdMult,
            };
        }
    }
}