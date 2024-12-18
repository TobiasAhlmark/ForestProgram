
namespace ForestProgram.Models;

public partial class Enviroment
{
    public int EnviromentId { get; set; }

    public int? ForestAreaId { get; set; }

    public string? GroundType { get; set; }

    public string? Temperature { get; set; }

    public string? Precipitation { get; set; }

    public string? Wind { get; set; }

    public string? Altitude { get; set; }

    public ForestArea forestArea { get; set; }
}
