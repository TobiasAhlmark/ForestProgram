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

    public OperationResult<TreeManagement> AddManagement(TreeManagement treeManagement, string location)
    {
        var forestAreas = _forestAreaService.GettAllForestAreas();

        var selectedForestArea = forestAreas.FirstOrDefault(area => area.Location == location);

        if (selectedForestArea == null)
        {
            return new OperationResult<TreeManagement>
            {
                Success = false,
                Message = "Forest not found",
            };
        }

        treeManagement.ForestAreaId = selectedForestArea.ForestAreaId;

        _forestProgramContext.TreeManagements.Add(treeManagement);
        _forestProgramContext.SaveChanges();

        return new OperationResult<TreeManagement>
        {
            Success = true,
            Message = $"You picked {selectedForestArea.Location}"
        };
    }
}