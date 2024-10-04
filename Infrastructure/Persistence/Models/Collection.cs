using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Collection
{
    public int Id { get; set; }

    public short IdLocation { get; set; }

    public short IdUser { get; set; }

    public int? IdReciclador { get; set; }

    public DateTime DateUpdated { get; set; }

    public decimal Latas { get; set; }

    public decimal Papel { get; set; }

    public decimal PlasticoOtros { get; set; }

    public decimal PlasticoPet { get; set; }

    public decimal Vidrio { get; set; }

    public decimal Tetrapak { get; set; }

    public DateTime Created { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
