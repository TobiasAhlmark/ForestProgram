using System;
using System.Collections.Generic;

namespace ForestProgram.Models;

public class TreeManagement
{
    public int TreeManagementId { get; set; }

    public int ForestAreId { get; set; }

    public int? TreeId { get; set; }

    public int? SpeciesId { get; set; }

    public string? NumberOfTreesTreated { get; set; }

    public string? Action { get; set; }

    public DateOnly? Date { get; set; }

    public string? Responsible { get; set; }

    public string? Method { get; set; }

    public string? Note { get; set; }

    public DateOnly? FollowUp { get; set; }
}
