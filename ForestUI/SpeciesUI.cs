using ForestProgram.Models;

namespace ForestProgram.UI;

public class SpeciesUI
{

    private readonly TreeService _treeService;
    private readonly ForestProgramDbContext _forestProgramContext;

    public SpeciesUI(ForestProgramDbContext context)
    {
        _forestProgramContext = context;
    }
    public void SpeciesMenu()
    {
        Console.WriteLine("Species Menu");
        Console.WriteLine("1. Add Species");
        Console.WriteLine("2. Update Species");
        Console.WriteLine("3. Display Species");
        Console.WriteLine("4. Get information about a species");

        if (int.TryParse(Console.ReadLine(), out int input))
        {
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
    }

    public void AddSpecies()
    {
        bool menu = true;
        while (menu)
        {
            Console.WriteLine("Lägg till Art");
            Console.Write("Art-namn:");
            string species = Console.ReadLine();
            Console.WriteLine("Barr eller lövträd");
            string type = Console.ReadLine();
            Console.WriteLine("Medellivsläng");
            string lifeSpan = Console.ReadLine();
            Console.WriteLine("Hur och vart trivs trädet");
            string adaptation = Console.ReadLine();

            Species species1 = new Species
            {
                Name = species,
                Type = type,
                LifeSpan = lifeSpan,
                Adaptation = adaptation
            };

            _forestProgramContext.Add(species1);
            _forestProgramContext.SaveChanges();
            Console.WriteLine($"{species} tillagd!");
            Console.WriteLine("Lägg till ny art (1) Återgå till meny (0)");
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