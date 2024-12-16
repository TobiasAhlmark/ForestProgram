using System;
using System.Collections.Generic;

namespace ForestProgram.Models;

public partial class Tree
{
    public int TreeId { get; set; }

    public string? ForestAreaId { get; set; }

    public int? SpeciesId { get; set; }

    public string? Height { get; set; }

    public int? Age { get; set; }

    public string? TrunkDiameter { get; set; }

    public string? Health { get; set; }

    public string? Location { get; set; }
}
