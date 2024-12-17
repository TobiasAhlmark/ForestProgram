using ForestProgram.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

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

        if (forestAreas.Success)
        {
            var selectedForestArea = forestAreas.Data.FirstOrDefault(area => area.Location == location);

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
        else
            return new OperationResult<TreeManagement>
            {
                Success = false,
                Message = "Forest not found",
            };
    }

    public OperationResult<List<TreeManagement>> GetAllTreeManagements()
    {
        var treemanagement = _forestProgramContext.TreeManagements
        .Include(tm => tm.ForestArea)
        .Include(tm => tm.Species)
        .ToList();

        if (treemanagement == null || !treemanagement.Any())
        {
            return new OperationResult<List<TreeManagement>>
            {
                Success = false,
                Message = "No tree management found!"
            };
        }
        return new OperationResult<List<TreeManagement>>
        {
            Success = true,
            Message = "",
            Data = treemanagement
        };
    }

    public OperationResult<TreeManagement> UpDateTreeManageMent(TreeManagement treeManagement)
    {
        var upDate = _forestProgramContext.TreeManagements
        .FirstOrDefault(tm => tm.TreeManagementId == treeManagement.TreeManagementId);

        if (upDate == null)
        {
            return new OperationResult<TreeManagement>
            {
                Success = false,
                Message = "Could not update treemanagement",
                Data = treeManagement
            };
        }
        upDate.NumberOfTreesTreated = treeManagement.NumberOfTreesTreated;
        upDate.Action = treeManagement.Action;
        upDate.Date = treeManagement.Date;
        upDate.Responsible = treeManagement.Responsible;
        upDate.Note = treeManagement.Note;
        upDate.FollowUp = treeManagement.FollowUp;

        _forestProgramContext.SaveChanges();

        return new OperationResult<TreeManagement>
        {
            Success = true,
            Message = "Management updated!",
            Data = treeManagement
        };
    }
}