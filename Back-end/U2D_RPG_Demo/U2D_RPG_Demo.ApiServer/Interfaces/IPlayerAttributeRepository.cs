using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Interfaces {
    public interface IPlayerAttributeRepository {
        Task<PlayerAttribute?> GetAttributeByIdAsync(int uid, CancellationToken cancellation);
        Task<PlayerAttribute?> CreateAttributeAsync(PlayerAttribute playerAttribute, CancellationToken cancellation);
        Task<List<PlayerAttribute>?> HardDeleteAttributesAsync(int id, CancellationToken cancellation); 
    }
}
