using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Event
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; }

    public string Description { get; set; } = null!;

    public string Address { get; set; } = null!;

    /// <summary>
    /// [E]vent, [R]ecycle
    /// </summary>
    public string Type { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime Finish { get; set; }

    public string Url { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Whatsapp { get; set; }

    /// <summary>
    /// waze url
    /// </summary>
    public string Waze { get; set; }

    public string IdCategories { get; set; }

    public string Materials { get; set; }

    public bool? Active { get; set; }

    public string IdPartners { get; set; }

    public DateTime Created { get; set; }
}
