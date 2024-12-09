using ForestProgram.Models;
using Microsoft.CodeAnalysis;

namespace ForestProgram.Services;

public class TreeManagementService
{
    private readonly ForestProgramDbContext _forestProgramContext;
    private readonly ForestAreaService _forestAreaService;

    public TreeManagementService(ForestProgramDbContext forestProgramDbContext, ForestAreaService forestAreaService)
    {
        _forestProgramContext = forestProgramDbContext;
        _forestAreaService = forestAreaService;
    }
    public OperationResult AddManagement(TreeManagement treeManagement, string location)
    {
        var forestAreas = _forestAreaService.GettAllForestAreas();

        var selectedForestArea = forestAreas.FirstOrDefault(area => area.Location == location);

        if (selectedForestArea == null)
        {
            return new OperationResult
            {
                Success = false,
                Message = "Forest not found"
            };
        }

        treeManagement.ForestAreId = selectedForestArea.ForestAreId;
        return new OperationResult
        {
            Success = true,
            Message = $"You picked {selectedForestArea.Location}"
        };




    }
}