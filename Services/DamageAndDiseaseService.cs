using ForestProgram.Models;

namespace ForestProgram.Services;

public class DamageAndDiseaseService
{
    private readonly ForestProgramDbContext _forestProgramDbContext;

    public DamageAndDiseaseService(ForestProgramDbContext forestProgramDbContext)
    {
        _forestProgramDbContext = forestProgramDbContext;
    }

    public void AddDamageAndDiseaseReport()
    {

    }

    public OperationResult<DamageAndDisease> AddDamageAndDiseaseToForestArea(DamageAndDisease damageAndDisease)
    {
        if(damageAndDisease == null)
        {
            return new OperationResult<DamageAndDisease>
            {
                Success = false,
                Message = "No report found!",
                Data = damageAndDisease
            };
        }

        _forestProgramDbContext.DamageAndDiseases.Add(damageAndDisease);
        _forestProgramDbContext.SaveChanges();

        damageAndDisease.Note = $"Report number {damageAndDisease.DamageAndDiseaseId}: {damageAndDisease.Note}";
        _forestProgramDbContext.SaveChanges();

        return new OperationResult<DamageAndDisease>
        {
            Success = true,
            Message = $"Report number {damageAndDisease.DamageAndDiseaseId} added to {damageAndDisease.forestArea.Location}",
            Data = damageAndDisease
        };
    }
}