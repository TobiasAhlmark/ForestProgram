using System;
using System.Collections.Generic;

namespace ForestProgram.Models;

public class Species
{
    public int SpeciesId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? LifeSpan { get; set; }

    public string? Adaptation { get; set; }
}
