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
        "View Environments by Forest Area",
        "Update Environment",
        "Delete Environment",
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
            case "View Environments by Forest Area":
                ViewEnvironmentsPrompt();
                break;
            case "Update Environment":
                UpdateEnvironmentPrompt();
                break;
            case "Delete Environment":
                DeleteEnvironmentPrompt();
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
        var forestArea = _forestAreaService.GettAllForestAreas();

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

        var handleSelectedArea = forestArea.Data
        .FirstOrDefault(f => $"{f.ForestAreaId} - {f.Location}" == selectedForestArea);

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

    private void ViewEnvironmentsPrompt()
    {
        throw new NotImplementedException();
    }

    private void UpdateEnvironmentPrompt()
    {
        throw new NotImplementedException();
    }

    private void DeleteEnvironmentPrompt()
    {
        throw new NotImplementedException();
    }
}