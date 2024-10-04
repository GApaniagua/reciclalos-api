using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Goal
{
    public int Id { get; set; }

    public int? IdPartner { get; set; }

    public DateOnly Start { get; set; }

    public DateOnly Finish { get; set; }

    public float Amount { get; set; }

    public bool? Visible { get; set; }

    public DateTime Created { get; set; }
}
