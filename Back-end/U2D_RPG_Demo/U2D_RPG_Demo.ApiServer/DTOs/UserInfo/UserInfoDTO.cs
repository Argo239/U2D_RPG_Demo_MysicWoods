using U2D_RPG_Demo.ApiServer.Models;

namespace U2D_RPG_Demo.ApiServer.Dtos.UserInfo
{
    public class UserInfoDTO {
        public int Uid { get; set; }

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string Password { get; set; } = null!;

        public string? Name { get; set; }

        public DateTime CreateTime { get; set; }

        public int HasDelete { get; set; }

        public DateTime? DeleteTime { get; set; }

        public DateTime LastUpdateTime { get; set; }
    }
}
