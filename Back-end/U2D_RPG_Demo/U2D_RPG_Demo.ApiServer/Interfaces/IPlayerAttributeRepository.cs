using U2D_RPG_Demo.ApiServer.DTOs.PlayerAttribute;
using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Interfaces {
    public interface IPlayerAttributeRepository {
        Task<PlayerAttributes?> GetAttributesByIdAsync(int uid, CancellationToken cancellation);
        Task<PlayerAttributes?> CreateAttributesAsync(PlayerAttributes playerAttribute, CancellationToken cancellation);
        Task<PlayerAttributes?> HardDeleteAttributesAsync(int id, CancellationToken cancellation); 
        Task<PlayerAttributes?> UpdateAttributesAsync(int id, UpdatePlayerAttributeRequestDTO playerAttribute, CancellationToken cancellation); 
    }
}
