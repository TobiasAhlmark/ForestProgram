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

    public OperationResult<List<DamageRepair>> GetAllRepairReports()
    {
        var repairList = _forestProgramDbContext.DamageRepairs.ToList();

        if(repairList == null)
        {
            return new OperationResult<List<DamageRepair>>
            {
                Success = false,
                Message = "No repair reports found!",
            };
        }
        return new OperationResult<List<DamageRepair>>
        {
            Success = true,
            Message = "",
            Data = repairList
        };
    }

    public OperationResult<DamageRepair> GetSpecifikRepairReportById(int id)
    {
        var specifikReport = _forestProgramDbContext.DamageRepairs
        .FirstOrDefault(sr => sr.DamageRepairId == id);

        if(specifikReport == null)
        {
            return new OperationResult<DamageRepair>
            {
                Success = false,
                Message = "Could not find any report with that id"
            };
        }

        return new OperationResult<DamageRepair>
        {
            Success = true,
            Message = "Succes!",
            Data = specifikReport
        };
    }

}