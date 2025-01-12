using System.IO.Compression;
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

        if (addRepairReport != null)
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

        if (repairList == null)
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

        if (specifikReport == null)
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

    public OperationResult<DamageRepair> UpdateRepairReport(DamageRepair damageRepair)
    {
        var findReport = _forestProgramDbContext.DamageRepairs
        .FirstOrDefault(r => r.DamageRepairId == damageRepair.DamageRepairId);

        if (findReport == null)
        {
            return new OperationResult<DamageRepair>
            {
                Success = false,
                Message = "Could not update report",
            };
        }

        findReport.Action = damageRepair.Action;
        findReport.Responsible = damageRepair.Responsible;
        findReport.TimeSpan = damageRepair.TimeSpan;
        findReport.Resources = damageRepair.Resources;
        findReport.Priority = damageRepair.Priority;
        findReport.Satus = damageRepair.Satus;
        findReport.FollowUp = damageRepair.FollowUp;
        findReport.Result = damageRepair.Result;

        return new OperationResult<DamageRepair>
        {
            Success = true,
            Data = findReport
        };

    }

    public OperationResult<DamageRepair> UpDateFollowUp(DamageRepair damageRepair)
    {
        var repair = _forestProgramDbContext.DamageRepairs
        .FirstOrDefault(r => r.DamageRepairId == damageRepair.DamageRepairId);

        if(repair == null)
        {
            return new OperationResult<DamageRepair>
            {
                Success = false,
                Message = "Could not find the report"
            };
        }

        repair.FollowUp = damageRepair.FollowUp;
        repair.Satus = damageRepair.Satus;
        repair.Result = damageRepair.Result;

        if (repair.DamageAndDisease != null)
        {
            var damageAndDisease = repair.DamageAndDisease;
            damageAndDisease.Severity = "Resolved"; 
            damageAndDisease.DateLastObservation = DateTime.Now;
        }

        _forestProgramDbContext.SaveChanges();

        return new OperationResult<DamageRepair>
        {
            Success = true,
            Message = "Follow-up successfully completed.",
            Data = repair
        };
    }

}