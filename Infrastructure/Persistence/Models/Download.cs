using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Download
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string File { get; set; } = null!;

    public short Position { get; set; }

    public DateTime Created { get; set; }
}
