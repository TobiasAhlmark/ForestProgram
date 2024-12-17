using ForestProgram.Models;
using Microsoft.CodeAnalysis;
using ForestProgram.Services;

namespace ForestProgram.UI;

public class SpeciesUI
{

    private readonly TreeService _treeService;
    private readonly ForestProgramDbContext _forestProgramContext;
    private readonly SpeciesService _speciesService;

    public SpeciesUI
    (
        ForestProgramDbContext context,
        SpeciesService speciesService,
        TreeService treeService
    )
    {
        _forestProgramContext = context;
        _speciesService = speciesService;
        _treeService = treeService;
    }
    public void SpeciesMenu()
    {
        Console.WriteLine("Species Menu");
        Console.WriteLine("1. Add Species ");
        Console.WriteLine("2. Update Species ");
        Console.WriteLine("3. Display Species ");
        Console.WriteLine("4. Get information about a species");

        int input = Utilities.GetValidIntInput("Enter Choice: ", "Use numbers, try again!");
        
            switch (input)
            {
                case 1:
                    AddSpecies();
                    break;
                case 2:
                    UpdateSpecies();
                    break;
                case 3:
                    DisplaySpecies();
                    break;
                case 4:
                    GetInfoSpecies();
                    break;

                default:
                    Console.WriteLine("Use numbers betwen 1-4");
                    break;
            }
    }

    public void AddSpecies()
    {
        bool menu = true;
        while (menu)
        {
            Species addSpecies = new();
            Console.WriteLine("Add Species");
           
            string species = Utilities.GetString("Enter species name: ", "Try again!");
            
            var result = _speciesService.GetSpeciesByName(species);

            if (result.Success)
            {
                Console.WriteLine($"{result.Message}");
                Console.WriteLine("Going back to menu!");
                break;
            }
            else if (!result.Success)
            {
                Console.WriteLine($"{result.Message}");
            }
            string type = Utilities.GetString("coniferous trees or deciduous tree: ", "Try again!");
            string lifeSpan = Utilities.GetString("Average lifespan: ", "Try again!");
            string adaptation = Utilities.GetString("Adaptation, how and where: ", "Try again!");

            addSpecies.Name = species;
            addSpecies.Type = type;
            addSpecies.LifeSpan = lifeSpan;
            addSpecies.Adaptation = adaptation;

            _speciesService.AddSpecies(addSpecies);

            Console.WriteLine($"added!");
            Console.WriteLine("Add new species (1) mainmenu (0)");
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (input == 0)
                {
                    menu = false;
                }
            }
        }
    }

    public void UpdateSpecies()
    {

    }

    public void DisplaySpecies()
    {
        Console.WriteLine("Sök efter Art");
        string searchSpecies = Console.ReadLine();
        var species = _forestProgramContext.Species.FirstOrDefault(s => s.Name.ToLower() == searchSpecies.ToLower());

        Console.WriteLine($"{species.Name}, {species.LifeSpan}, {species.Adaptation}");
    }

    public void GetInfoSpecies()
    {
        
    }

}