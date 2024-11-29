using System;
using System.Collections.Generic;

namespace ForestProgram.Models;

public partial class ForestArea
{
    public int ForestAreId { get; set; }

    public string ForestType { get; set; }

    public string? AreaSquareMeters { get; set; }

    public int? Age { get; set; }

    public string? Location { get; set; }

    public string? EcoSystem { get; set; }
}
