using System;
using System.Collections.Generic;

namespace U2D_RPG_Demo.ApiServer.Models;

public partial class UserInfo
{
    public int UID { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Password { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime CreateTime { get; set; }

    public int HasDelete { get; set; }

    public DateTime? DeleteTime { get; set; }

    public DateTime LastUpdateTime { get; set; }

    public virtual ICollection<PlayerAttributes> PlayerAttributes { get; set; } = new List<PlayerAttributes>();
}
