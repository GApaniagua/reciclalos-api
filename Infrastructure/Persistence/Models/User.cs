using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class User
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    /// <summary>
    /// Admin-User
    /// </summary>
    public string Type { get; set; } = null!;

    public string IdLocations { get; set; } = null!;

    /// <summary>
    /// usuarios acopiadores
    /// </summary>
    public string? IdUsers { get; set; }

    public string Logo { get; set; } = null!;

    public DateTime? Created { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
}
