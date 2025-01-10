using ForestProgram.Models;
using Microsoft.EntityFrameworkCore;

namespace ForestProgram.Services;

public class DamageAndDiseaseService
{
    private readonly ForestProgramDbContext _forestProgramDbContext;

    public DamageAndDiseaseService(ForestProgramDbContext forestProgramDbContext)
    {
        _forestProgramDbContext = forestProgramDbContext;
    }

    public OperationResult<DamageAndDisease> AddDamageAndDiseaseReportToForestArea(DamageAndDisease damageAndDisease)
    {
        if (damageAndDisease == null)
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

    public OperationResult<List<DamageAndDisease>> GetAllDamageAndDiseaseReports()
    {
        var reportList = _forestProgramDbContext.DamageAndDiseases
        .Include(f => f.forestArea)
        .Include(s => s.species)
        .ToList();

        if (reportList == null)
        {
            return new OperationResult<List<DamageAndDisease>>
            {
                Success = false,
                Message = "Could not find any reports!",
                Data = reportList
            };
        }

        return new OperationResult<List<DamageAndDisease>>
        {
            Success = true,
            Message = "Reports found!",
            Data = reportList
        };
    }

    public OperationResult<DamageAndDisease> UpdateDamageAndDisease(DamageAndDisease damageAndDisease)
    {
        var update = _forestProgramDbContext.DamageAndDiseases
        .FirstOrDefault(dmg => dmg.DamageAndDiseaseId == damageAndDisease.DamageAndDiseaseId);

        if (update == null)
        {
            return new OperationResult<DamageAndDisease>
            {
                Success = false,
                Message = "No report found!"
            };
        }
        update.ForestAreaId = damageAndDisease.ForestAreaId;
        update.TreeId = damageAndDisease.TreeId;
        update.SpeciesId = damageAndDisease.SpeciesId;
        update.DamageAndDiseaseType = damageAndDisease.DamageAndDiseaseType;
        update.Symptom = damageAndDisease.Symptom;
        update.Severity = damageAndDisease.Severity;
        update.Reason = damageAndDisease.Reason;
        update.Spread = damageAndDisease.Spread;
        update.DateFirstObservation = damageAndDisease.DateFirstObservation;
        update.DateLastObservation = damageAndDisease.DateLastObservation;
        update.Note = damageAndDisease.Note;

        _forestProgramDbContext.SaveChanges();

        return new OperationResult<DamageAndDisease>
        {
            Success = true,
            Message = $"Report {update.DamageAndDiseaseId} Updated!",
            Data = update
        };
    }

}