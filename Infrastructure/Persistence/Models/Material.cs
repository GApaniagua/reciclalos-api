using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ImageUrl { get; set; }

    public string Color { get; set; } = null!;
}
