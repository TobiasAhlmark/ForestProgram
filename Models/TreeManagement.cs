
namespace ForestProgram.Models;

public partial class TreeManagement
{
    public int TreeManagementId { get; set; }

    public int ForestAreaId { get; set; }

    public int? TreeId { get; set; }

    public int? SpeciesId { get; set; }

    public string? NumberOfTreesTreated { get; set; }

    public string? Action { get; set; }

    public DateTime? Date { get; set; }

    public string? Responsible { get; set; }

    public string? Method { get; set; }

    public string? Note { get; set; }

    public DateTime? FollowUp { get; set; }

    public ForestArea ForestArea { get; set; }
    public Species Species { get; set; }
}
