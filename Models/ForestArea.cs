﻿

namespace ForestProgram.Models;

public partial class ForestArea
{
    public int ForestAreaId { get; set; }

    public string ForestType { get; set; } = null;

    public string? AreaSquareMeters { get; set; }

    public int? Age { get; set; }

    public string? Location { get; set; }

    public string? EcoSystem { get; set; }

    public ICollection<Enviroment> Enviroments { get; set; } = new List<Enviroment>();
}
