using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using U2D_RPG_Demo.ApiServer.Interfaces;
using U2D_RPG_Demo.ApiServer.Mappers;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Repository {
    public class PlayerAttributeRepository : IPlayerAttributeRepository {

        private readonly DataContext _dataContext;

        public PlayerAttributeRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public async Task<PlayerAttribute?> GetAttributeByIdAsync(int uid, CancellationToken cancellation) {
            var playerAttributeModel = await _dataContext.PlayerAttributes.FindAsync(uid, cancellation);


            var ddd = await _dataContext.PlayerAttributes.Where(pa => pa.Uid == id)

            return playerAttributeModel == null ? null : playerAttributeModel;
        }

        public async Task<List<PlayerAttribute>?> HardDeleteAttributesAsync(int id, CancellationToken cancellation) {
            var playerAttributeModel = await _dataContext.PlayerAttributes
                .Where(pa => pa.Uid == id)
                .ToListAsync(cancellation);

            if (playerAttributeModel == null)
                return null;

            _dataContext.PlayerAttributes.RemoveRange(playerAttributeModel);

            await _dataContext.SaveChangesAsync(cancellation);

            return playerAttributeModel;
        }

        public async Task<PlayerAttribute?> CreateAttributeAsync(PlayerAttribute playerAttributeModel ,CancellationToken cancellation) {
            await _dataContext.PlayerAttributes.AddAsync(playerAttributeModel, cancellation);
            await _dataContext.SaveChangesAsync(cancellation);
            return playerAttributeModel;
        }
    }
}
