using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Category
{
    public sbyte Id { get; set; }

    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;

    public DateTime Created { get; set; }
}
