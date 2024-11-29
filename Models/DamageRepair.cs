using System;
using System.Collections.Generic;

namespace ForestProgram.Models;

public partial class DamageRepair
{
    public int DamageRepairId { get; set; }

    public int? DamageAndDiseaseId { get; set; }

    public string? Action { get; set; }

    public string? Responsible { get; set; }

    public string? TimeSpan { get; set; }

    public string? Resources { get; set; }

    public string? Priority { get; set; }

    public string? Satus { get; set; }

    public string? FollowUp { get; set; }

    public string? Result { get; set; }
}
