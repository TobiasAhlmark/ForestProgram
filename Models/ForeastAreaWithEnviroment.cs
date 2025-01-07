


namespace ForestProgram.Models;
public class ForestAreaWithEnviroments
{
    public int ForestAreaId { get; set; }
    public string Location { get; set; }
    public List<Enviroment> Enviroments { get; set; } = new List<Enviroment>();
    public List<DamageAndDisease> DamageAndDiseases { get; internal set; }
}