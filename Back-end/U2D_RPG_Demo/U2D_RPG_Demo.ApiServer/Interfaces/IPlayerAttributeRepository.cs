using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Interfaces {
    public interface IPlayerAttributeRepository {

        Task<List<PlayerAttribute>?> HardDeletePlayaerAttributesAsync(int id, CancellationToken cancellation); 
    }
}
