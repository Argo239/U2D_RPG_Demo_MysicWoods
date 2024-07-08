using System.Security.Permissions;
using U2D_RPG_Demo.ApiServer.Dtos.UserInfo;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Interfaces {
    public interface IUserInfoRepository {

        Task<List<UserInfo>> GetAllAsync(CancellationToken cancellation);
        Task<UserInfo?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<UserInfo?> CreateUserAsync(UserInfo userInfoModel, CancellationToken cancellation);
        Task<UserInfo?> UpdateUserInfoAsync(int id, UpdataUserInfoRequestDTO updateDTO, CancellationToken cancellation);
        Task<UserInfo?> SoftDeleteUserInfoAsync(int id, SoftDeleteUserInfoRequestDTO softDeleteDTO, CancellationToken cancellation);
        Task<UserInfo?> HardDeleteUserInfoAsync(int id, CancellationToken cancellation);
    }
}
