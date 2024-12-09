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
    public OperationResult AddForestArea(ForestArea forestArea)
    {
        /*
        SELECT COUNT(*)
        FROM ForestAreas
        WHERE Location = @Location;
        */
        var existingForestArea = _forestProgramContext.ForestAreas.FirstOrDefault(f => f.Location == forestArea.Location);
        if (existingForestArea == null)
        {
            return new OperationResult
            {
                Success = false,
                Message = $"A forest area with location '{forestArea.Location}' does not exists."
            };
        }

        if (int.TryParse(forestArea.AreaSquareMeters, out int squareMeter))
        {
            if (squareMeter < 1000)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Squaremeter to low, need to be atleast 1000 squaremeter."
                };
            }
        }
        else
        {
            return new OperationResult
            {
                Success = false,
                Message = $"Invalid square meter value"
            };
        }

        if (forestArea.Age > 300)
        {
            return new OperationResult
            {
                Success = false,
                Message = $"This forrest seems a little bit to old"
            };
        }

        /*
        SQL-kod:
        INSERT INTO ForestAreas (Location, ForestType, AreaSquareMeters, EcoSystem, Age)
        VALUES (@Location, @ForestType, @AreaSquareMeters, @EcoSystem, @Age);
        */
        _forestProgramContext.ForestAreas.Add(forestArea);
        _forestProgramContext.SaveChanges();

        return new OperationResult
        {
            Success = true,
            Message = $"{forestArea.Location} Logging added!"
        };

    }

    public List<ForestArea> GettAllForestAreas()
    {
        return _forestProgramContext.ForestAreas.ToList();
    }
}