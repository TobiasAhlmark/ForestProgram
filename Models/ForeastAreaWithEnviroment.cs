


namespace ForestProgram.Models;
public class ForestAreaWithEnviroments
{
    public int ForestAreaId { get; set; }
    public string Location { get; set; }
    public List<Enviroment> Enviroments { get; set; } 
    public List<DamageAndDisease> DamageAndDiseases { get; set; }
    public List<DamageRepair> RepairReports { get; set; }
}