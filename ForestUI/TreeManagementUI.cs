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
        var menuOptions = new List<string>
    {
        "Add management",
        "Update management",
        "Show managements",
        "Exit"
    };

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(menuOptions)
        );

        switch (selectedOption)
        {
            case "Add management":
                AddManagement();
                break;
            case "Update management":
                UpdateManagement();
                break;
            case "Show managements":
                ShowInfoManagement();
                break;
            case "Exit":
                Console.WriteLine("Exiting the program...");
                break;
            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }

    public void AddManagement()
    {
        var menuOptions = new List<string>
    {
        "Logging",
        "Thinning",
        "Planting",
        "Other (Enter manually)",
        "Update management",
        "Exit"
    };

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select action")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(menuOptions)
        );

        switch (selectedOption)
        {
            case "Logging":
                Logging();
                break;
            case "Thinning":
                Thinning();
                break;
            case "Planting":
                Planting();
                break;
            case "Other (Enter manually)":
                EnterManually();
                break;
            case "Update management":
                UpdateManagement();
                break;
            case "Exit":
                Console.WriteLine("Exiting the program...");
                break;
            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }

    public void Logging()
    {
        TreeManagement treeManagement = new TreeManagement();

        treeManagement.Action = "Logging";

        DateTime startdate = Utilities.GetValidDate("Startdate: ");
        treeManagement.Date = startdate;

        string squareMeter = Utilities.GetString("Enter squaremeters: ", "Try again!");
        treeManagement.NumberOfTreesTreated = squareMeter;

        string responsible = Utilities.GetString("Enter name of the person in charge of the action: ", "try again");
        treeManagement.Responsible = responsible;

        Console.WriteLine("Write note: ");
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
                Console.WriteLine("Logging management added");
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

        DateTime startdate = Utilities.GetValidDate("Startdate: ");
        treeManagement.Date = startdate;

        string squareMeter = Utilities.GetString("Enter squaremeters: ", "Try again!");
        treeManagement.NumberOfTreesTreated = squareMeter;

        string responsible = Utilities.GetString("Enter name of the person in charge of the action: ", "try again");
        treeManagement.Responsible = responsible;

        Console.WriteLine("Write note: ");
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
                Console.WriteLine("Thinning management added");
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

        DateTime startdate = Utilities.GetValidDate("Startdate: ");
        treeManagement.Date = startdate;

        string squareMeter = Utilities.GetString("Enter squaremeters: ", "Try again!");
        treeManagement.NumberOfTreesTreated = squareMeter;

        string numberOfTrees = Utilities.GetString("Enter estimated number of plants: ", "Try again!");
        treeManagement.NumberOfTreesTreated = numberOfTrees;

        while (true)
        {
            // Hämta alla arter
            Console.WriteLine("Select species: ");
            var speciesService = new SpeciesService(_forestProgramContext);
            var speciesList = speciesService.GetAllSpecies();

            // Presentera en lista med arter för användaren att välja från
            var speciesNames = speciesList.Select(s => s.Name).ToList();
            speciesNames.Add("Exit");


            string selectedSpeciesName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose a species to plant")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                    .AddChoices(speciesNames)
            );
            if (selectedSpeciesName.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting the program...");
                break;
            }

            //Hämtar den valda arten
            var selectedSpecies = speciesService.GetSpeciesByName(selectedSpeciesName);

            if (selectedSpecies.Success)
            {
                treeManagement.SpeciesId = selectedSpecies.Data.SpeciesId;
            }
        }

        string responsible = Utilities.GetString("Enter name of the person in charge of the action: ", "try again");
        treeManagement.Responsible = responsible;

        Console.WriteLine("Write a note: ");
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
                Console.WriteLine($"Planting management added to location {menuResult.Data}");
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

        Console.WriteLine("Select location");

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

            string action = Utilities.GetString("Enter action: ", "Try again!");
            treeManagement.Action = action;

            DateTime startdate = Utilities.GetValidDate("Startdate: ");
            treeManagement.Date = startdate;

            string squareMeter = Utilities.GetString("Enter squaremeters or estimated number of trees: ", "Try again!");
            treeManagement.NumberOfTreesTreated = squareMeter;

            string responsible = Utilities.GetString("Enter name of the person in charge of the action: ", "try again");
            treeManagement.Responsible = responsible;

            Console.WriteLine("Write note: ");
            string note = Console.ReadLine();
            if (note == "")
            {
                treeManagement.Note = "No note written.";
            }
            treeManagement.Note = note;

            var result = _treeManagementService.AddManagement(treeManagement, selectedLocation);

            if (result.Success)
            {
                Console.WriteLine($"Tree management added to location {result.Data}");
                break;
            }
            else
            {
                Console.WriteLine($"Error, location {result.Data}");
            }
        }
    }

    public void UpdateManagement()
    {
        var manageMentResult = _treeManagementService.GetAllTreeManagements();

        if (manageMentResult.Success)
        {
            // Skapa en lista med managements som ska visas i menyn
            var managementOptions = manageMentResult.Data
                .Select(tm => $"{tm.ForestArea.Location} - {tm.Action}")
                .ToList();

            managementOptions.Add("Exit");

            var selectedManagement = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select management to update")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                    .AddChoices(managementOptions)
            );
            if (selectedManagement.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting the program...");
                return;
            }

            var selectedTreeManagement = manageMentResult.Data
            .FirstOrDefault(tm => $"{tm.ForestArea.Location} - {tm.Action}" == selectedManagement);

            if (selectedTreeManagement != null)
            {
                var UpdateOptions = new List<string>
                {
                   "Number of trees",
                   "Species",
                   "Action",
                   "StartDate",
                   "Responsible",
                   "Note",
                   "Follow up",
                   "Exit"
                };

                var selectedUpdate = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Select option to update")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                    .AddChoices(UpdateOptions)
                );
                if (selectedUpdate.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting the program...");
                    return;
                }

                switch (selectedUpdate)
                {
                    case "Number of trees":
                        Console.WriteLine($"Past info {selectedTreeManagement.NumberOfTreesTreated}");
                        string numberOfTrees = Utilities.GetString("Enter updated number of trees: ", "Try Again!");
                        selectedTreeManagement.NumberOfTreesTreated = numberOfTrees;
                        break;
                    case "Species":
                        Console.WriteLine($"Past info {selectedTreeManagement.Species.Name}");
                        var speciesList = _forestProgramContext.Species.ToList();
                        var speciesOptions = speciesList
                        .Select(s => s.Name)
                        .ToList();
                        speciesOptions.Add("Exit");

                        var selectedSpecies = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select species")
                            .PageSize(10) // Antal alternativ som visas åt gången
                            .MoreChoicesText("[grey](Use arrow keys to scroll)[/]")
                            .AddChoices(speciesOptions)
                        );
                        if (selectedSpecies.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Exiting species selection...");
                            break;
                        }

                        var selectedSpeciesName = speciesList.FirstOrDefault(s => s.Name == selectedSpecies);

                        if (selectedSpecies != null)
                        {
                            selectedTreeManagement.Species = selectedSpeciesName;
                            Console.WriteLine($"Species updated to: {selectedSpeciesName.Name}");
                        }
                        else
                        {
                            Console.WriteLine("Species not found.");
                        }
                        break;
                    case "Action":
                        Console.WriteLine($"Past info {selectedTreeManagement.Action}");
                        string action = Utilities.GetString("Enter updated actino: ", "Try Again!");
                        selectedTreeManagement.Action = action;
                        break;
                    case "StartDate":
                        Console.WriteLine($"Past info {selectedTreeManagement.Date}");
                        DateTime startDate = Utilities.GetValidDate("Enter new startdate yyyy-MM-DD: ");
                        selectedTreeManagement.Date = startDate;
                        break;
                    case "Responsible":
                        Console.WriteLine($"Past info {selectedTreeManagement.Responsible}");
                        string responsible = Utilities.GetString("Enter responsible: ", "Try again!");
                        selectedTreeManagement.Responsible = responsible;
                        break;
                    case "Note":
                        Console.WriteLine($"Past info {selectedTreeManagement.Note}");
                        string note = Utilities.GetString("Enter note: ", "Try Again!");
                        selectedTreeManagement.Note = note;
                        break;
                    case "Follow up":
                        Console.WriteLine($"Past info {selectedTreeManagement.FollowUp}");
                        DateTime followUp = Utilities.GetValidDate("Enter follow up date yyyy-MM-dd: ");
                        selectedTreeManagement.FollowUp = followUp;
                        break;

                    default:
                        Console.WriteLine("");
                        break;
                }
                var updatedResult = _treeManagementService.UpDateTreeManageMent(selectedTreeManagement);

                if (updatedResult.Success)
                {
                    Console.WriteLine($"{updatedResult.Message}");
                }
                else
                {
                    Console.WriteLine($"Failed updated {updatedResult.Data}");
                }
            }
            else
            {
                Console.WriteLine("No Tree management found!");
            }
        }
    }

    public void ShowInfoManagement()
    {
        var showTreeManagement = _treeManagementService.GetAllTreeManagements();

        if (showTreeManagement.Success)
        {
            foreach (var info in showTreeManagement.Data)
            {
                // Hantera null-värden med null-conditionals och defaultvärden
                var location = info.ForestArea?.Location ?? "Unknown location";
                var speciesName = info.Species?.Name ?? "Unknown species";
                var action = info.Action ?? "No action specified";
                var date = info.Date?.ToString("yyyy-MM-dd") ?? "No date specified";
                var responsible = info.Responsible ?? "No responsible person";
                var note = info.Note ?? "No notes";
                var numberOfTrees = info.NumberOfTreesTreated ?? "No trees treated";

                Console.WriteLine($"{location}\n{action}\n{date}\n{responsible}\n{note}\n{speciesName}\n{numberOfTrees}\n--------------");
            }
        }
        else
        {
            Console.WriteLine(showTreeManagement.Message);
        }
    }
}