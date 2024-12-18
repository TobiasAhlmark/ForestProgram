
namespace ForestProgram.Models;

public partial class PlantingHistory
{
    public int PlantingHistoryId { get; set; }

    public int? SpeciesId { get; set; }

    public string? NumberOfTreesPlanted { get; set; }

    public DateOnly? Date { get; set; }

    public string? PlantedBy { get; set; }

    public string? Note { get; set; }
}
