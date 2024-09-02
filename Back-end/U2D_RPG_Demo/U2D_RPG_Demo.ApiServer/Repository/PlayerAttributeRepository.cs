using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using U2D_RPG_Demo.ApiServer.DTOs.PlayerAttribute;
using U2D_RPG_Demo.ApiServer.Interfaces;
using U2D_RPG_Demo.ApiServer.Mappers;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Repository {
    public class PlayerAttributeRepository : IPlayerAttributeRepository {

        private readonly DataContext _dataContext;

        public PlayerAttributeRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public async Task<PlayerAttributes?> GetAttributesByIdAsync(int uid, CancellationToken cancellation) {
            return await _dataContext.PlayerAttributes
                .Where(pa => pa.UID == uid)
                .FirstOrDefaultAsync(cancellation);
        }

        public async Task<PlayerAttributes?> HardDeleteAttributesAsync(int uid, CancellationToken cancellation) {
            var playerAttributeModel = await _dataContext.PlayerAttributes
                .Where(pa => pa.UID == uid)
                .FirstOrDefaultAsync(cancellation);

            if (playerAttributeModel == null)
                return null;

            _dataContext.PlayerAttributes.Remove(playerAttributeModel);

            await _dataContext.SaveChangesAsync(cancellation);

            return playerAttributeModel;
        }

        public async Task<PlayerAttributes?> CreateAttributesAsync(PlayerAttributes playerAttributeModel, CancellationToken cancellation) {
            await _dataContext.PlayerAttributes.AddAsync(playerAttributeModel, cancellation);
            await _dataContext.SaveChangesAsync(cancellation);
            return playerAttributeModel;
        }

        public async Task<PlayerAttributes?> UpdateAttributesAsync(int uid, UpdatePlayerAttributeRequestDTO updateDTO, CancellationToken cancellation) {
            var playerAttributeModel = await _dataContext.PlayerAttributes
                .Where(pa => pa.UID == uid)
                .FirstOrDefaultAsync(cancellation);

            if (playerAttributeModel == null)
                return null;

            {
                playerAttributeModel.Level = updateDTO.Level;   
                playerAttributeModel.Experience = updateDTO.Experience; 
                playerAttributeModel.Hp = updateDTO.Hp;
                playerAttributeModel.MaxMp = updateDTO.MaxMp;
                playerAttributeModel.Mp = updateDTO.Mp;
                playerAttributeModel.MaxMp = updateDTO.MaxMp;
                playerAttributeModel.ATK = updateDTO.ATK;
                playerAttributeModel.DEF = updateDTO.DEF;
                playerAttributeModel.DR = updateDTO.DR;
                playerAttributeModel.SPD = updateDTO.SPD;
                playerAttributeModel.SPDMult = updateDTO.SPDMult;   

            }

            return playerAttributeModel;
        }
    }
}
