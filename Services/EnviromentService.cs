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
        var addEnviroment = GetAllEnviroments();

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
}