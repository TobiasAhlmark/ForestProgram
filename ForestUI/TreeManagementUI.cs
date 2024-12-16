using ForestProgram.Models;
using ForestProgram.Services;
using Spectre.Console;

namespace ForestProgram.UI;

public class TreeManagementUI
{
    private readonly ForestProgramDbContext _forestProgramContext;
    private readonly TreeManagementService _treeManagementService;
    private readonly ForestAreaService _forestAreaService;

    public TreeManagementUI
    (
    ForestProgramDbContext context,
    TreeManagementService treeManagementService,
    ForestAreaService forestAreaService
    )

    {
        _forestProgramContext = context;
        _treeManagementService = treeManagementService;
        _forestAreaService = forestAreaService;
    }

    public void ManagementMenu()
    {
        Console.WriteLine("1. Add management");
        Console.WriteLine("2. Update management");
        Console.WriteLine("3. Show info about specifik management");

        if (int.TryParse(Console.ReadLine(), out int input))
        {
            switch (input)
            {
                case 1:
                    AddManagement();
                    break;
                case 2:
                    UpdateManagement();
                    break;
                case 3:
                    ShowInfoManagement();
                    break;

                default:
                    Console.WriteLine("Use numbers betwen 1-3");
                    break;
            }
        }
    }

    public void AddManagement()
    {
        Console.WriteLine("Select action:");
        Console.WriteLine("1. Logging");
        Console.WriteLine("2. Thinning");
        Console.WriteLine("3. Planting");
        Console.WriteLine("4. Other (Enter manually)");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            switch (choice)
            {
                case 1:
                    Logging();
                    break;
                case 2:
                    Thinning();
                    break;
                case 3:
                    Planting();
                    break;
                case 4:
                    EnterManually();
                    break;

                default:
                    Console.WriteLine("Use numbers betwen 1-3");
                    break;
            }
        }
    }

    public void Logging()
    {
        TreeManagement treeManagement = new TreeManagement();

        treeManagement.Action = "Logging";

        DateTime startdate = Utilities.GetValidDate("Startdate");
        treeManagement.Date = startdate;

        string squareMeter = Utilities.GetString("Enter squaremeters", "Try again!");
        treeManagement.NumberOfTreesTreated = squareMeter;

        string responsible = Utilities.GetString("Enter name of the person in charge of the action", "try again");
        treeManagement.Responsible = responsible;

        string note = Console.ReadLine();
        treeManagement.Note = string.IsNullOrEmpty(note) ? "No note written." : note;

        while (true)
        {
            var forestAreas = _forestAreaService.GettAllForestAreas();

            var areaLocations = forestAreas
            .Select(area => area.Location)
            .ToList();

            areaLocations.Add("Exit");

            var selectedLocation = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a Forest Area Location")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(areaLocations)
            );

            if (selectedLocation.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting the program...");
                break;
            }

            var menuResult = _treeManagementService.AddManagement(treeManagement, selectedLocation);

            if (menuResult.Success)
            {
                Console.WriteLine("Tree management added");
                break;
            }
            else
            {
                Console.WriteLine(menuResult.Message);
            }
        }
    }

    public void Thinning()
    {
        TreeManagement treeManagement = new TreeManagement();

        treeManagement.Action = "Thinning";

        DateTime startdate = Utilities.GetValidDate("Startdate");
        treeManagement.Date = startdate;

        string squareMeter = Utilities.GetString("Enter squaremeters", "Try again!");
        treeManagement.NumberOfTreesTreated = squareMeter;

        string responsible = Utilities.GetString("Enter name of the person in charge of the action", "try again");
        treeManagement.Responsible = responsible;

        string note = Console.ReadLine();
        treeManagement.Note = string.IsNullOrEmpty(note) ? "No note written." : note;

        while (true)
        {
            var forestAreas = _forestAreaService.GettAllForestAreas();

            var areaLocations = forestAreas
            .Select(area => area.Location)
            .ToList();

            areaLocations.Add("Exit");

            var selectedLocation = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a Forest Area Location")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(areaLocations)
            );

            if (selectedLocation.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting the program...");
                break;
            }

            var menuResult = _treeManagementService.AddManagement(treeManagement, selectedLocation);

            if (menuResult.Success)
            {
                Console.WriteLine("Tree management added");
                break;
            }
            else
            {
                Console.WriteLine(menuResult.Message);
            }
        }
    }

    public void Planting()
    {
        TreeManagement treeManagement = new TreeManagement();

        treeManagement.Action = "Planting";

        DateTime startdate = Utilities.GetValidDate("Startdate ");
        treeManagement.Date = startdate;

        string squareMeter = Utilities.GetString("Enter squaremeters ", "Try again!");
        treeManagement.NumberOfTreesTreated = squareMeter;

        string numberOfTrees = Utilities.GetString("Enter estimated number of plants ", "Try again!");
        treeManagement.NumberOfTreesTreated = numberOfTrees;

        // Hämta alla arter
        Console.WriteLine("Select species: ");
        var speciesService = new SpeciesService(_forestProgramContext);
        var speciesList = speciesService.GetAllSpecies();

        // Presentera en lista med arter för användaren att välja från
        var speciesNames = speciesList.Select(s => s.Name).ToList();

        
        string selectedSpeciesName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a species to plant")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(speciesNames)
        );

        //Hämtar den valda arten
        var selectedSpecies = speciesService.GetSpeciesByName(selectedSpeciesName);
        
        treeManagement.SpeciesId =  
        
        string responsible = Utilities.GetString("Enter name of the person in charge of the action", "try again");
        treeManagement.Responsible = responsible;

        Console.WriteLine("Write a note:");
        string note = Console.ReadLine();
        treeManagement.Note = string.IsNullOrEmpty(note) ? "No note written." : note;

        while (true)
        {
            var forestAreas = _forestAreaService.GettAllForestAreas();

            var areaLocations = forestAreas
            .Select(area => area.Location)
            .ToList();

            areaLocations.Add("Exit");

            var selectedLocation = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a Forest Area Location")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(areaLocations)
            );

            if (selectedLocation.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting the program...");
                break;
            }

            var menuResult = _treeManagementService.AddManagement(treeManagement, selectedLocation);

            if (menuResult.Success)
            {
                Console.WriteLine("Tree management added");
                break;
            }
            else
            {
                Console.WriteLine(menuResult.Message);
            }
        }
    }

    public void EnterManually()
    {
        TreeManagement treeManagement = new TreeManagement();

        var forestAreas = _forestAreaService.GettAllForestAreas();

        var areaLocations = forestAreas
        .Select(area => area.Location)
        .ToList();

        var selectedLocation = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Choose a Forest Area Location")
            .PageSize(10)
            .MoreChoicesText("[grey](Use arrow keys to select)[/]")
            .AddChoices(areaLocations)
        );

        var menuResult = _treeManagementService.AddManagement(treeManagement, selectedLocation);

        if (menuResult.Success)
        {
            Console.Write("Enter what your action is: ");
            treeManagement.Action = Console.ReadLine();
        }
        else
        {
            Console.WriteLine(menuResult.Message);
        }

        DateTime startdate = Utilities.GetValidDate("Startdate");
        treeManagement.Date = startdate;

        string squareMeter = Utilities.GetString("Enter squaremeters or estimated number of trees", "Try again!");
        treeManagement.NumberOfTreesTreated = squareMeter;

        string responsible = Utilities.GetString("Enter name of the person in charge of the action", "try again");
        treeManagement.Responsible = responsible;

        string note = Console.ReadLine();
        if (note == "")
        {
            treeManagement.Note = "No note written.";
        }
        treeManagement.Note = note;
    }

    public void UpdateManagement()
    {

    }

    public void ShowInfoManagement()
    {

    }
}