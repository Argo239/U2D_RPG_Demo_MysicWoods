using Microsoft.EntityFrameworkCore;
using U2D_RPG_Demo.ApiServer.Interfaces;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Repository {
    public class PlayerAttributeRepository : IPlayerAttributeRepository {
        private readonly DataContext _dataContext;

        public PlayerAttributeRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public async Task<List<PlayerAttribute>?> HardDeletePlayaerAttributesAsync(int id, CancellationToken cancellation) {
            var playerAttributeModel = await _dataContext.PlayerAttributes
                .Where(pa => pa.Uid == id)
                .ToListAsync(cancellation);

            if (playerAttributeModel == null)
                return null;

            _dataContext.PlayerAttributes.RemoveRange(playerAttributeModel);

            await _dataContext.SaveChangesAsync(cancellation);

            return playerAttributeModel;
        }
    }
}
