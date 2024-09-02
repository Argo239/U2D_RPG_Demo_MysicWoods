using U2D_RPG_Demo.ApiServer.Dtos.PlayerAttribute;
using U2D_RPG_Demo.ApiServer.Dtos.UserInfo;
using U2D_RPG_Demo.ApiServer.DTOs.PlayerAttribute;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Mappers
{
    public static class PlayerAttributeMappers {

        public static PlayerAttributeDTO ToPlayerAttributeDTO(this PlayerAttributes playerAttributeModel) {
            return new PlayerAttributeDTO {
                PAID = playerAttributeModel.PAID,
                UID = playerAttributeModel.UID,
                Level = playerAttributeModel.Level,
                Experience = playerAttributeModel.Experience,
                Hp = playerAttributeModel.Hp,
                MaxHp = playerAttributeModel.MaxHp,
                Mp = playerAttributeModel.Mp,
                MaxMp = playerAttributeModel.MaxMp,
                ATK = playerAttributeModel.ATK,
                DEF = playerAttributeModel.DEF,
                DR = playerAttributeModel.DR,
                SPD = playerAttributeModel.SPD,
                SPDMult = playerAttributeModel.SPDMult,
            };
        }

        public static PlayerAttributes ToCreatePlayerAttributeDTO(this CreatePlayerAttributeRequestDTO playerAttributeDTO, int uid) {
            return new PlayerAttributes {
                UID = uid,
                Level = playerAttributeDTO.Level,
                Experience = playerAttributeDTO.Experience,
                Hp = playerAttributeDTO.Hp,
                MaxHp = playerAttributeDTO.MaxHp,
                Mp = playerAttributeDTO.Mp,
                MaxMp = playerAttributeDTO.MaxMp,
                ATK = playerAttributeDTO.ATK,
                DEF = playerAttributeDTO.DEF,
                DR = playerAttributeDTO.DR,
                SPD = playerAttributeDTO.SPD,
                SPDMult = playerAttributeDTO.SPDMult,
            };
        }

    }
}