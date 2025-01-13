
using ForestProgram.Models;

namespace ForestProgram.Services;

public class ForestAreaService
{
    private readonly ForestProgramDbContext _forestProgramContext;

    public ForestAreaService(ForestProgramDbContext forestProgramDbContext)
    {
        _forestProgramContext = forestProgramDbContext;
    }
    public OperationResult<ForestArea> AddForestArea(ForestArea forestArea)
    {
        var existingForestArea = _forestProgramContext.ForestAreas.FirstOrDefault(f => f.Location == forestArea.Location);
        if (existingForestArea == null)
        {
            return new OperationResult<ForestArea>
            {
                Success = false,
                Message = $"A forest area with location '{forestArea.Location}' does not exists."
            };
        }

        if (int.TryParse(forestArea.AreaSquareMeters, out int squareMeter))
        {
            if (squareMeter < 1000)
            {
                return new OperationResult<ForestArea>
                {
                    Success = false,
                    Message = $"Squaremeter to low, need to be atleast 1000 squaremeter."
                };
            }
        }
        else
        {
            return new OperationResult<ForestArea>
            {
                Success = false,
                Message = $"Invalid square meter value"
            };
        }

        if (forestArea.Age > 300)
        {
            return new OperationResult<ForestArea>
            {
                Success = false,
                Message = $"This forrest seems a little bit to old"
            };
        }

        _forestProgramContext.ForestAreas.Add(forestArea);
        _forestProgramContext.SaveChanges();

        return new OperationResult<ForestArea>
        {
            Success = true,
            Message = $"{forestArea.Location} Logging added!"
        };

    }

    public OperationResult<List<ForestArea>> GetAllForestAreas()
    {
        var allForestAreas = _forestProgramContext.ForestAreas.ToList();

        if (allForestAreas == null)
        {
            return new OperationResult<List<ForestArea>>
            {
                Success = false,
                Message = "No Areas found!",
                Data = null
            };
        }
        return new OperationResult<List<ForestArea>>
        {
            Success = true,
            Message = "Areas Found!",
            Data = allForestAreas
        };
    }

    public OperationResult<List<ForestAreaWithEnviroments>> GetForestAreaWithEnviromentAndDamageAndDisease()
    {
        var getAreasAndEnviroment = _forestProgramContext.ForestAreas
         .Select(fa => new ForestAreaWithEnviroments
         {
             ForestAreaId = fa.ForestAreaId,
             Location = fa.Location,
             
             Enviroments = _forestProgramContext.Enviroments
                .Where(e => e.ForestAreaId == fa.ForestAreaId)
                .ToList(),
             DamageAndDiseases = _forestProgramContext.DamageAndDiseases
                .Where(dd => dd.ForestAreaId == fa.ForestAreaId)
                .ToList(),
             RepairReports = _forestProgramContext.DamageRepairs
                .Where(r => r.DamageAndDiseaseId == fa.DamageAndDiseases.DamageAndDiseaseId)
                .ToList()

         })
            .GroupBy(fa => fa.ForestAreaId) // Gruppera efter ID
            .Select(g => g.First()) // Ta endast en instans per grupp
            .ToList();

        if (getAreasAndEnviroment == null)
        {
            return new OperationResult<List<ForestAreaWithEnviroments>>
            {
                Success = false,
                Message = "Could not get any results!"
            };
        }
        return new OperationResult<List<ForestAreaWithEnviroments>>
        {
            Success = true,
            Data = getAreasAndEnviroment
        };
    }

    public OperationResult<ForestArea> GetForestAreaOjbect(ForestArea forestArea)
    {
        var forestAreas = _forestProgramContext.ForestAreas
        .FirstOrDefault(f => f.ForestAreaId == forestArea.ForestAreaId);

        if (forestArea == null)
        {
            return new OperationResult<ForestArea>
            {
                Success = false,
                Message = "No forestarea found"
            };
        }
        return new OperationResult<ForestArea>
        {
            Success = true,
            Message = "",
            Data = forestArea
        };
    }

    public OperationResult<ForestArea> UpdateForestArea(ForestArea forestArea)
    {
        var Update = _forestProgramContext.ForestAreas
        .FirstOrDefault(f => f.ForestAreaId == forestArea.ForestAreaId);

        if (Update == null)
        {
            return new OperationResult<ForestArea>
            {
                Success = false,
                Message = "No forest found",
                Data = forestArea
            };
        }

        Update.ForestType = forestArea.ForestType;
        Update.AreaSquareMeters = forestArea.AreaSquareMeters;
        Update.Age = forestArea.Age;
        Update.Location = forestArea.Location;
        Update.EcoSystem = forestArea.EcoSystem;

        _forestProgramContext.SaveChanges();

        return new OperationResult<ForestArea>
        {
            Success = true,
            Message = "ForestArea updated!",
            Data = forestArea
        };
    }

    public OperationResult<ForestArea> GetSpecifikForestArea(ForestArea forestArea)
    {
        var specifikForestArea = _forestProgramContext.ForestAreas
        .FirstOrDefault(fa => fa.ForestAreaId == forestArea.ForestAreaId);

        if (specifikForestArea == null)
        {
            return new OperationResult<ForestArea>
            {
                Success = false,
                Message = "No forestarea found!"
            };
        }
        else
        {
            return new OperationResult<ForestArea>
            {
                Success = true,
                Message = "Forestarea found!",
                Data = specifikForestArea
            };
        }
    }

}