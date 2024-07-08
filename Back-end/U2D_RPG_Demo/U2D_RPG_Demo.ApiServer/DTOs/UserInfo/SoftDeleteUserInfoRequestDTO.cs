namespace U2D_RPG_Demo.ApiServer.Dtos.UserInfo {
    public class SoftDeleteUserInfoRequestDTO {

        public int HasDelete { get; set; }

        public DateTime? DeleteTime { get; set; }

        public DateTime LastUpdateTime { get; set; }
    }
}
