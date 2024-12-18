
namespace ForestProgram.Models;

public partial class DamageAndDisease
{
    public int DamageAndDiseaseId { get; set; }

    public int? ForestAreaId { get; set; }

    public int? TreeId { get; set; }

    public int? SpeciesId { get; set; }

    public string? DamageAndDiseaseType { get; set; }

    public string? Symptom { get; set; }

    public string? Severity { get; set; }

    public string? Reason { get; set; }

    public string? Spread { get; set; }

    public DateOnly? DateFirstObservation { get; set; }

    public DateOnly? DateLastObservation { get; set; }

    public string? Note { get; set; }
}
