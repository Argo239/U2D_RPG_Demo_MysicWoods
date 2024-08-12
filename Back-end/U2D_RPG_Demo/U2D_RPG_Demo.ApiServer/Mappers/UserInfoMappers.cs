using Microsoft.CodeAnalysis.CSharp.Syntax;
using U2D_RPG_Demo.ApiServer.Dtos.UserInfo;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Mappers
{
    public static class UserInfoMappers {

        public static UserInfoDTO ToUserInfoDTO(this UserInfo userInfoModel) {
            return new UserInfoDTO {
                Uid = userInfoModel.Uid,
                Email = userInfoModel.Email,
                Phone = userInfoModel.Phone,
                Password = userInfoModel.Password,
                Name = userInfoModel.Name,
                CreateTime = userInfoModel.CreateTime,
                HasDelete = userInfoModel.HasDelete,
                DeleteTime = userInfoModel.DeleteTime,
                LastUpdateTime = userInfoModel.LastUpdateTime
            };
        }

        public static UserInfo ToUserInfoFromCreateDTO(this CreateUserInfoRequestDTO userInfoDTO) {
            return new UserInfo {
                Email = userInfoDTO.Email,
                Phone = userInfoDTO.Phone,
                Password = userInfoDTO.Password,
                Name = userInfoDTO.Name,
                CreateTime = DateTime.UtcNow,
                LastUpdateTime = DateTime.UtcNow
            };
        }
    }
}
