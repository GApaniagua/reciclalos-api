using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Location
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Image { get; set; }

    public string Schedule { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Phone2 { get; set; } = null!;

    public string Whatsapp { get; set; } = null!;

    public string Materials { get; set; } = null!;

    public string? Latlng { get; set; }

    public string? Departamento { get; set; }

    public string? Municipio { get; set; }

    public string Email { get; set; } = null!;

    public string IdPartners { get; set; } = null!;

    public string Estacion { get; set; } = null!;

    public bool? Active { get; set; }

    public bool? Aprobado { get; set; }

    public DateTime Created { get; set; }

    public int IdUser { get; set; }
}
