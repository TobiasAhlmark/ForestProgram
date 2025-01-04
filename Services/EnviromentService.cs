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

        if(addEnviroment == null)
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

    public List<Enviroment> GetAllEnviroments()
    {
        return _dbContext.Enviroments.ToList();
    }

    public Enviroment GetSpecificEnviroment(int enviromentId)
    {
        return _dbContext.Enviroments.FirstOrDefault(E => E.EnviromentId == enviromentId);
    }

    public Enviroment GetEnviromentByForestAreaId(int forestAreaId)
    {
        return _dbContext.Enviroments.FirstOrDefault(e => e.ForestAreaId == forestAreaId);
    }
}