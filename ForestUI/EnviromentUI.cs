using ForestProgram.Models;
using ForestProgram.Services;
using Spectre.Console;

namespace ForestProgram.UI;

public class EnviromentUI
{
    private readonly ForestProgramDbContext _forestProgramContext;
    private readonly ForestAreaService _forestAreaService;
    private readonly EnviromentService _enviromentService;

    public EnviromentUI
    (
        ForestProgramDbContext forestProgramDbContext,
        ForestAreaService forestAreaService,
        EnviromentService enviromentService
    )
    {
        _forestProgramContext = forestProgramDbContext;
        _forestAreaService = forestAreaService;
        _enviromentService = enviromentService;
    }

    public void EnvironmentMenu()
    {
        var menuOptions = new List<string>
    {
        "Add Environment",
        "Update Environment",
        "Exit"
    };

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option from the Environment Menu")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(menuOptions)
        );

        switch (selectedOption)
        {
            case "Add Environment":
                AddEnvironment();
                break;
            case "Update Environment":
                UpdateEnvironment();
                break;
            case "Exit":
                Console.WriteLine("Exiting the Environment Menu...");
                break;
            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }

    private void AddEnvironment()
    {
        Enviroment enviroment = new();
        Console.WriteLine("Choose foreastArea: ");
        var forestArea = _forestAreaService.GetAllForestAreas();

        if (!forestArea.Success)
        {
            Console.WriteLine(forestArea.Message);
        }
        var forestAreaOptions = forestArea.Data
       .Select(fa => $"{fa.ForestAreaId} - {fa.Location}")
       .ToList();

        forestAreaOptions.Add("Exit");

        var selectedForestArea = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a Forest Area")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(forestAreaOptions)
        );

        if (selectedForestArea.Equals("Exit"))
        {
            Console.WriteLine("Exiting...");
            return;
        }

        var handleSelectedArea = forestArea.Data
        .FirstOrDefault(f => $"{f.ForestAreaId} - {f.Location}" == selectedForestArea);

        enviroment.forestArea = handleSelectedArea;
        enviroment.ForestAreaId = handleSelectedArea.ForestAreaId;

        string groundType = Utilities.GetString("Ground type: ", "Try again!");
        enviroment.GroundType = groundType;
        string temperature = Utilities.GetString("Enter average temperature: ", "Try again!");
        enviroment.Temperature = temperature;
        string precipitation = Utilities.GetString("Enter average precipitation in mm: ", "Try again!");
        enviroment.Precipitation = precipitation;
        string wind = Utilities.GetString("Enter average wind m/s: ", "Try again!");
        enviroment.Wind = wind;
        string altitude = Utilities.GetString("Enter altitude: ", "Try again!");
        enviroment.Altitude = altitude;

        _enviromentService.AddEnviroment(enviroment);

    }

    private void UpdateEnvironment()
    {
        Console.WriteLine("Choose foreastArea where u want to update enviroment: ");
        var forestArea = _forestAreaService.GetAllForestAreas();

        if (!forestArea.Success)
        {
            Console.WriteLine(forestArea.Message);
        }
        var forestAreaOptions = forestArea.Data
       .Select(fa => $"{fa.ForestAreaId} - {fa.Location}")
       .ToList();

        forestAreaOptions.Add("Exit");

        var selectedForestArea = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a Forest Area")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(forestAreaOptions)
        );

        if (selectedForestArea.Equals("Exit", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Exiting...");
            return;
        }

        var enviromentUpdate = forestArea.Data
        .FirstOrDefault(f => $"{f.ForestAreaId} - {f.Location}" == selectedForestArea);

        var enviroment = _enviromentService.GetEnviromentByForestAreaId(enviromentUpdate.ForestAreaId);

        if (!enviroment.Success)
        {
            Console.WriteLine(enviroment.Message);
            return;
        }

        Console.WriteLine("Choose the property of the environment to update:");

        var updateOptions = new List<string>
        {
            "GroundType",
            "Temperature",
            "Precipitation",
            "Wind",
            "Altitude",
            "Exit"
        };

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a property to update")
                .PageSize(6)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(updateOptions)
        );

        if (selectedOption.Equals("Exit"))
        {
            Console.WriteLine("Exiting...");
            return;
        }

        switch (selectedOption)
        {
            case "GroundType":
                Console.WriteLine($"Past info {enviroment.Data.GroundType}");
                Console.Write("Enter new GroundType: ");
                var newGroundType = Console.ReadLine();
                enviroment.Data.GroundType = newGroundType;
                Console.WriteLine($"New info added {enviroment.Data.forestArea}");
                Console.WriteLine(enviroment.Data.GroundType);
                Console.ReadKey();
                break;

            case "Temperature":
                Console.WriteLine($"Past info {enviroment.Data.Temperature}");
                Console.Write("Enter new Temperature: ");
                var newTemperature = Console.ReadLine();
                enviroment.Data.Temperature = newTemperature;
                Console.WriteLine($"New info added {enviroment.Data.forestArea}");
                Console.WriteLine(enviroment.Data.Temperature);
                Console.ReadKey();
                break;

            case "Precipitation":
                Console.WriteLine($"Past info {enviroment.Data.Precipitation}");
                Console.Write("Enter new Precipitation: ");
                var newPrecipitation = Console.ReadLine();
                enviroment.Data.Precipitation = newPrecipitation;
                Console.WriteLine($"New info added {enviroment.Data.forestArea}");
                Console.WriteLine(enviroment.Data.Precipitation);
                Console.ReadKey();
                break;

            case "Wind":
                Console.WriteLine($"Past info {enviroment.Data.Wind}");
                Console.Write("Enter new Wind: ");
                var newWind = Console.ReadLine();
                enviroment.Data.Wind = newWind;
                Console.WriteLine($"New info added {enviroment.Data.forestArea}");
                Console.WriteLine(enviroment.Data.Wind);
                Console.ReadKey();
                break;

            case "Altitude":
                Console.WriteLine($"Past info {enviroment.Data.Altitude}");
                Console.Write("Enter new Altitude: ");
                var newAltitude = Console.ReadLine();
                enviroment.Data.Altitude = newAltitude;
                Console.WriteLine($"New info added {enviroment.Data.forestArea}");
                Console.WriteLine(enviroment.Data.Altitude);
                Console.ReadKey();
                break;

            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }

}