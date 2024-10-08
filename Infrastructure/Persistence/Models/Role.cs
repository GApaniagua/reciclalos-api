﻿using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
}
