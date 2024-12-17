using ForestProgram.UI;
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

    public OperationResult<List<ForestArea>> GettAllForestAreas()
    {
        var allForestAreas = _forestProgramContext.ForestAreas.ToList();

        if (allForestAreas == null)
        {
            return new OperationResult<List<ForestArea>>
            {
                Success = false,
                Message = "No Areas found!"
            };
        }
        return new OperationResult<List<ForestArea>>
        {
            Success = true,
            Message = "Areas Found!",
            Data = allForestAreas
        };
    }

    public OperationResult<List<ForestAreaWithEnviroments>> GetForestAreaWithEnviroment()
    {
        var getAreasAndEnviroment = _forestProgramContext.ForestAreas
         .Select(fa => new ForestAreaWithEnviroments
         {
             ForestAreaId = fa.ForestAreaId,
             Location = fa.Location,
             // Hämta alla relaterade Enviroments för varje ForestArea
             Enviroments = _forestProgramContext.Enviroments
                    .Where(e => e.ForestAreaId == fa.ForestAreaId)
                    .ToList()
         })
            .ToList(); // Gippy tack tack

        if(getAreasAndEnviroment == null)
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
}