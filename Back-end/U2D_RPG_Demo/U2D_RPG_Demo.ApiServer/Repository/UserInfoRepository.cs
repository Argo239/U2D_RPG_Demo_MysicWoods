using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using U2D_RPG_Demo.ApiServer.Dtos.UserInfo;
using U2D_RPG_Demo.ApiServer.Interfaces;
using U2D_RPG_Demo.ApiServer.Mappers;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Repository {
    public class UserInfoRepository : IUserInfoRepository {

        private readonly DataContext _dataContext;

        public UserInfoRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public async Task<UserInfo?> CreateUserAsync(UserInfo userInfoModel, CancellationToken cancellation) {
            await _dataContext.UserInfo.AddAsync(userInfoModel, cancellation);
            await _dataContext.SaveChangesAsync(cancellation);
            return userInfoModel;
        }

        public async Task<List<UserInfo>> GetAllAsync(CancellationToken cancellation) {
            return await _dataContext.UserInfo
                .Where(u => u.HasDelete == 0)
                .ToListAsync(cancellation);
        }

        public async Task<UserInfo?> GetByIdAsync(int id, CancellationToken cancellation) {
            var userInfoModel = await _dataContext.UserInfo
                .Where(u => u.HasDelete == 0)
                .FirstOrDefaultAsync(u => u.UID == id);

            return userInfoModel == null ? null : userInfoModel;
        }

        public async Task<UserInfo?> HardDeleteUserInfoAsync(int id, CancellationToken cancellation) {
            var userInfoModel = await _dataContext.UserInfo
                .Where(u => u.HasDelete == 0)
                .FirstOrDefaultAsync (u => u.UID == id);    
            
            if (userInfoModel == null) 
                return null;
              
            _dataContext.UserInfo.Remove(userInfoModel);

            await _dataContext.SaveChangesAsync(cancellation);

            return userInfoModel;
        }

        public async Task<UserInfo?> SoftDeleteUserInfoAsync(int id, SoftDeleteUserInfoRequestDTO softDeleteDTO, CancellationToken cancellation) {
            var userInfoModel = await _dataContext.UserInfo
                .Where(u => u.HasDelete == 0)
                .FirstOrDefaultAsync(u => u.UID == id);

            if (userInfoModel == null) 
                return null;

            {
                userInfoModel.HasDelete = softDeleteDTO.HasDelete;
                userInfoModel.DeleteTime = softDeleteDTO.DeleteTime;
                userInfoModel.LastUpdateTime = softDeleteDTO.LastUpdateTime;    
            }

            await _dataContext.SaveChangesAsync(cancellation);
            return userInfoModel;
        }

        public async Task<UserInfo?> UpdateUserInfoAsync(int id, UpdataUserInfoRequestDTO updateDTO, CancellationToken cancellation) {
            var userInfoModel = await _dataContext.UserInfo
                .Where(u => u.UID == id)
                .FirstOrDefaultAsync(u => u.UID == id);

            if (userInfoModel == null)
                return null;

            {
                userInfoModel.Phone = updateDTO.Phone;
                userInfoModel.Password = updateDTO.Password;
                userInfoModel.Name = updateDTO.Name;
                userInfoModel.LastUpdateTime = updateDTO.LastUpdateTime;
            }
            
            return userInfoModel;
        }
    }
}

