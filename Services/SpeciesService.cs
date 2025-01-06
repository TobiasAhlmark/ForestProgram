using ForestProgram.Models;

namespace ForestProgram.Services;
public class SpeciesService
{
    private readonly ForestProgramDbContext _dbContext;

    public SpeciesService(ForestProgramDbContext dbContext)
    {
        _dbContext = dbContext;
    }
   
    public OperationResult<List<Species>> GetAllSpecies()
    {
        var species = _dbContext.Species.ToList();

        if (species == null || !species.Any())
        {
            return new OperationResult<List<Species>>
            {
                Success = false,
                Message = "No species where found"
            };
        }
        return new OperationResult<List<Species>>
        {
            Success = true,
            Message = "Succes!",
            Data = species
        };
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
                Data = species
            };
        }
        return new OperationResult<Species>
            {
                Success = true,
                Message = "Species already exists!",
                Data = species
            };
    }

    public OperationResult<Species> AddSpecies(Species species)
    {
        var addSpecies = _dbContext.Species
        .FirstOrDefault(s => s.Name.ToLower() == species.Name.ToLower());

        if(addSpecies != null)
        {
             return new OperationResult<Species>
            {
                Success = false,
                Message = "Species already exists!",
                Data = addSpecies
            };
        }

        _dbContext.Add(species);
        _dbContext.SaveChanges();

        return new OperationResult<Species>
        {
            Success = true,
            Message = "Species added!",
            Data = addSpecies
        };
    }

    public OperationResult<Species> UpdateSpecies(Species species)
    {
        var Update = _dbContext.Species
        .FirstOrDefault(s => s.SpeciesId == species.SpeciesId);

        if(Update == null)
        {
            return new OperationResult<Species>
            {
                Success = false,
                Message = "Could not update Species"
            };
        }
        Update.Name = species.Name;
        Update.Adaptation = species.Adaptation;
        Update.LifeSpan = species.LifeSpan;
        Update.Type = species.Type;

        _dbContext.SaveChanges();

        return new OperationResult<Species>
        {
            Success = true,
            Message = "Species updated!",
            Data = species
        };
    }
}
