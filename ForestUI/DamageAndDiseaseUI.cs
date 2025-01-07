using ForestProgram.Models;
using ForestProgram.Services;
using Spectre.Console;

namespace ForestProgram.UI;

public class DamageAndDiseaseUI
{
    private readonly ForestProgramDbContext _forestProgramContext;
    private readonly DamageAndDiseaseService _damageAndDiseaseService;
    private readonly ForestAreaService _forestAreaService;
    private readonly SpeciesService _speciesService;

    public DamageAndDiseaseUI
    (
        ForestProgramDbContext forestProgramDbContext,
        DamageAndDiseaseService damageAndDiseaseService,
        ForestAreaService forestAreaService,
        SpeciesService speciesService
    )
    {
        _forestProgramContext = forestProgramDbContext;
        _damageAndDiseaseService = damageAndDiseaseService;
        _forestAreaService = forestAreaService;
        _speciesService = speciesService;
    }
    public void DamageAndDiseaseMenu()
    {
        var menuOptions = new List<string>
    {
        "Add Damage or Disease Report",
        "View All Damage and Disease Reports",
        "View Specific Damage or Disease Report",
        "Update Damage or Disease Report",
        "Delete Damage or Disease Report",
        "Exit"
    };

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option from the Damage and Disease Menu")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(menuOptions)
        );

        switch (selectedOption)
        {
            case "Add Damage or Disease Report":
                AddDamageOrDiseaseReport();
                break;
            case "View All Damage and Disease Reports":
                ViewAllDamageAndDiseaseReports();
                break;
            case "View Specific Damage or Disease Report":
                ViewSpecificDamageOrDiseaseReport();
                break;
            case "Update Damage or Disease Report":
                UpdateDamageOrDiseaseReport();
                break;
            case "Delete Damage or Disease Report":
                DeleteDamageOrDiseaseReport();
                break;
            case "Exit":
                Console.WriteLine("Exiting the Damage and Disease Menu...");
                break;
            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }

    private void AddDamageOrDiseaseReport()
    {

        var speciesList = _speciesService.GetAllSpecies();

        if (!speciesList.Success)
        {
            Console.WriteLine(speciesList.Message);
        }

        var speciesOptions = speciesList.Data
        .Select(s => s.Name)
        .ToList();

        speciesOptions.Add("Exit");

        var selectedSpecies = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Select a species")
            .PageSize(10)
            .MoreChoicesText("[grey](Use arrow keys to select)[/]")
            .AddChoices(speciesOptions)
    );

        if (selectedSpecies.Equals("Exit", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Exiting the program...");
            return;
        }

        var handleSpecie = speciesList.Data
        .FirstOrDefault(s => s.Name == selectedSpecies);


        string type = Utilities.GetString("Enter the type of damage or disease: ", "Try again!");
        string symptom = Utilities.GetString("Enter symptoms: ", "Try again!");
        string severity = Utilities.GetString("Enter severity on a scale 1-10: ", "Try again!");
        string reason = Utilities.GetString("Enter reason: ", "try again!");
        string spread = Utilities.GetString("Enter spread: ", "Try again!");
        DateTime dateFirstObservation = Utilities.GetValidDate("Enter first observation date yyyy-DD-mm: ");
        DateTime dateLastObservation = Utilities.GetValidDate("Enter last observationd date yyyy-DD-mm: ");
        string note = Utilities.GetString("Enter note: ", "Try again!");

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

        var damageAndDisease = new DamageAndDisease
        {
            SpeciesId = handleSpecie.SpeciesId,
            ForestAreaId = handleSelectedArea.ForestAreaId,
            DamageAndDiseaseType = type,
            Symptom = symptom,
            Severity = severity,
            Reason = reason,
            Spread = spread,
            DateFirstObservation = dateFirstObservation,
            DateLastObservation = dateLastObservation,
            Note = note
        };

        var result = _damageAndDiseaseService.AddDamageAndDiseaseToForestArea(damageAndDisease);

    }

    private void ViewAllDamageAndDiseaseReports()
    {
        // Implementation for viewing all reports
    }

    private void ViewSpecificDamageOrDiseaseReport()
    {
        // Implementation for viewing a specific report
    }

    private void UpdateDamageOrDiseaseReport()
    {
        // Implementation for updating a report
    }

    private void DeleteDamageOrDiseaseReport()
    {
        // Implementation for deleting a report
    }
}
