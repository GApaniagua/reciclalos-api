using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Aliado
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public sbyte Position { get; set; }

    public DateTime Created { get; set; }
}
