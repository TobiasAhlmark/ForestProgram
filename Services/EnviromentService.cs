using ForestProgram.Models;

namespace ForestProgram.Services;

public class EnviromentService
{
    private readonly ForestProgramDbContext _dbContext;

    public EnviromentService(ForestProgramDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public OperationResult<Enviroment> AddEnviroment(Enviroment enviroment)
    {
        _dbContext.Enviroments.Add(enviroment);
        _dbContext.SaveChanges();
        var addEnviroment = GetSpecificEnviroment(enviroment.EnviromentId);

        if (addEnviroment == null)
        {
            return new OperationResult<Enviroment>
            {
                Success = false,
                Message = "Could not add Enviroment!",
                Data = enviroment
            };
        }

        return new OperationResult<Enviroment>
        {
            Success = true,
            Message = "",
            Data = enviroment
        };
    }

    public OperationResult<List<Enviroment>> GetAllEnviroments()
    {
        var result = _dbContext.Enviroments.ToList();

        if (result == null)
        {
            return new OperationResult<List<Enviroment>>
            {
                Success = false,
                Message = "No enviroment found!",
                Data = result
            };
        }

        return new OperationResult<List<Enviroment>>
        {
            Success = true,
            Message = "",
            Data = result
        };
    }

    public OperationResult<Enviroment> GetSpecificEnviroment(int enviromentId)
    {
        var result = _dbContext.Enviroments.FirstOrDefault(E => E.EnviromentId == enviromentId);

        if (result == null)
        {
            return new OperationResult<Enviroment>
            {
                Success = false,
                Message = "No enviroment found!",
                Data = result
            };
        }

        return new OperationResult<Enviroment>
        {
            Success = true,
            Message = "",
            Data = result
        };
    }

    public OperationResult<Enviroment> GetEnviromentByForestAreaId(int forestAreaId)
    {
        var result = _dbContext.Enviroments.FirstOrDefault(e => e.ForestAreaId == forestAreaId);

        if (result == null)
        {
            return new OperationResult<Enviroment>
            {
                Success = false,
                Message = "No enviroment found!",
                Data = result
            };
        }

        return new OperationResult<Enviroment>
        {
            Success = true,
            Message = "",
            Data = result
        };
    }
}