using System;
using System.Collections.Generic;

namespace PRN232_SU25_SE182614.Repositories.Models;

public partial class Handbag
{
    public int HandbagId { get; set; }

    public int? BrandId { get; set; }

    public string ModelName { get; set; } = null!;

    public string? Material { get; set; }

    public string? Color { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public virtual Brand? Brand { get; set; }
}
