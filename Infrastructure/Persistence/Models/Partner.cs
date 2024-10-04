using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Partner
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string IdLocations { get; set; } = null!;

    public bool? Active { get; set; }

    public DateTime Created { get; set; }
}
