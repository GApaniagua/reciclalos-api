using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Question1 { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
