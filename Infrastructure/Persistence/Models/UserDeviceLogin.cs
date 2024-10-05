using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class UserDeviceLogin
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Device { get; set; }

    public string Os { get; set; }

    public string AppVersion { get; set; }

    public DateTime? CreatedAt { get; set; }
}
