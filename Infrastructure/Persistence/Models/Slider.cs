using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Slider
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public sbyte Position { get; set; }

    public DateTime Created { get; set; }
}
