using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Benefit
{
    public int Id { get; set; }

    public short IdPartner { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateOnly Start { get; set; }

    public DateOnly Finish { get; set; }

    public string Url { get; set; } = null!;

    public bool? Active { get; set; }

    /// <summary>
    /// Pendiente,Aprobado
    /// </summary>
    public string Status { get; set; } = null!;

    public DateTime Created { get; set; }
}
