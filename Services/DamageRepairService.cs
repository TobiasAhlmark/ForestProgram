using ForestProgram.Models;
using ForestProgram.UI;

namespace ForestProgram.Services;

public class DamageRepairService
{
    private readonly ForestProgramDbContext _forestProgramDbContext;

    public DamageRepairService(ForestProgramDbContext forestProgramDbContext)
    {
        _forestProgramDbContext = forestProgramDbContext;
    }

    public OperationResult<DamageRepair> AddDamageRepair(DamageRepair damageRepair)
    {
        var addRepairReport = _forestProgramDbContext.DamageRepairs
        .FirstOrDefault(rep => rep.DamageRepairId == damageRepair.DamageRepairId);

        if(addRepairReport != null)
        {
            return new OperationResult<DamageRepair>
            {
                Success = false,
                Message = "This report id already exists",
                Data = damageRepair
            };
        }
        else
        {
            _forestProgramDbContext.Add(damageRepair);
            _forestProgramDbContext.SaveChanges();

            return new OperationResult<DamageRepair>
            {
                Success = true,
                Message = $"Report id: {damageRepair.DamageRepairId} added!",
                Data = damageRepair
            };
        }
    }
}