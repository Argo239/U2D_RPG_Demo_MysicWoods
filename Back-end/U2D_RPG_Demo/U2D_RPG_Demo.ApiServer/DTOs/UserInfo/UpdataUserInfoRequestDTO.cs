namespace U2D_RPG_Demo.ApiServer.Dtos.UserInfo {
    public class UpdataUserInfoRequestDTO {
        public string? Phone { get; set; }

        public string Password { get; set; } = null!;

        public string? Name { get; set; }

        public DateTime LastUpdateTime { get; set; }
    }
}
