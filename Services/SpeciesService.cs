using ForestProgram.Models;

public class SpeciesService
{
    private readonly ForestProgramDbContext _dbContext;

    public SpeciesService(ForestProgramDbContext dbContext)
    {
        _dbContext = dbContext;
    }
   
    public List<Species> GetAllSpecies()
    {
        return _dbContext.Species.ToList(); 
    }

    public OperationResult<Species> GetSpeciesByName(string name)
    {  
        var species = _dbContext.Species.FirstOrDefault(s => s.Name.ToLower() == name.ToLower()); // Hämtar art baserat på namn

        if (species == null)
        {
            return new OperationResult<Species>
            {
                Success = false,
                Message = "Species name not found",
                Data = null
            };
        }
        return new OperationResult<Species>
            {
                Success = false,
                Message = "Species name found",
                Data = species
            };
    }

    public OperationResult<Species> AddSpecies(Species species)
    {
        
        _dbContext.Add(species);
        _dbContext.SaveChanges();



        return new OperationResult<Species>
            {
                Success = false,
                Message = "Species name not found",
                Data = null
            };
    }
}
