using ForestProgram.Models;
using ForestProgram.Services;
using Spectre.Console;

namespace ForestProgram.UI;

public class DamageAndDiseaseUI
{
    private readonly DamageAndDiseaseService _damageAndDiseaseService;
    private readonly ForestAreaService _forestAreaService;
    private readonly SpeciesService _speciesService;

    public DamageAndDiseaseUI
    (
        DamageAndDiseaseService damageAndDiseaseService,
        ForestAreaService forestAreaService,
        SpeciesService speciesService
    )
    {
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
        damageAndDisease.species = handleSpecie;
        damageAndDisease.forestArea = handleSelectedArea;

        var result = _damageAndDiseaseService.AddDamageAndDiseaseReportToForestArea(damageAndDisease);

        if (result.Success)
        {
            Console.WriteLine(result.Message);
        }
        else
        {
            Console.WriteLine(result.Message);
        }
        Console.ReadKey();
    }

    private void ViewAllDamageAndDiseaseReports()
    {
        var result = _damageAndDiseaseService.GetAllDamageAndDiseaseReports();

        if (!result.Success)
        {
            Console.WriteLine(result.Message);
        }
        else
        {
            foreach (var report in result.Data)
            {
                Console.WriteLine($"Report : {report.DamageAndDiseaseId} - Location: {report.forestArea.Location}");
                Console.WriteLine($"Species: {report.species.Name} - Severity: {report.Severity}");
                Console.WriteLine($"Type   : {report.DamageAndDiseaseType} - Symptom: {report.Symptom}");
                Console.WriteLine($"Reason : {report.Reason} - Spread: {report.Spread}");
                Console.WriteLine("First Observation: " + report.DateFirstObservation?.ToString("yyyy-MM-dd"));
                Console.WriteLine("Last Observation : " + report.DateLastObservation?.ToString("yyyy-MM-dd"));
            }
        }
        Console.ReadKey();
    }

    private void ViewSpecificDamageOrDiseaseReport()
    {
        var damageAndDisease = _damageAndDiseaseService.GetAllDamageAndDiseaseReports();

        if (!damageAndDisease.Success)
        {
            Console.WriteLine(damageAndDisease.Message);
        }
        else
        {
            var options = damageAndDisease.Data
            .Select(dmg => $"Report id: {dmg.DamageAndDiseaseId} Location: {dmg.forestArea.Location ?? "Unknown"}")
            .ToList();

            options.Add("Exit");

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select a report")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(options)
            );

            var selected = damageAndDisease.Data
            .FirstOrDefault(r => $"Report id: {r.DamageAndDiseaseId} Location: {r.forestArea?.Location ?? "Unknown"}" == selectedOption);

            if (selected != null)
            {
                Console.WriteLine($"Species: {selected.species.Name} - Severity: {selected.Severity}");
                Console.WriteLine($"Type   : {selected.DamageAndDiseaseType} - Symptom: {selected.Symptom}");
                Console.WriteLine($"Reason : {selected.Reason} - Spread: {selected.Spread}");
                Console.WriteLine("First Observation: " + selected.DateFirstObservation?.ToString("yyyy-MM-dd"));
                Console.WriteLine("Last Observation : " + selected.DateLastObservation?.ToString("yyyy-MM-dd"));
            }
            else
            {
                Console.WriteLine("No valid report was selected!");
            }
        }
        Console.ReadKey();
    }

    private void UpdateDamageOrDiseaseReport()
    {
        var damageAndDisease = _damageAndDiseaseService.GetAllDamageAndDiseaseReports();

        if (!damageAndDisease.Success)
        {
            Console.WriteLine(damageAndDisease.Message);
        }
        else
        {
            var options = damageAndDisease.Data
            .Select(dmg => $"Report id: {dmg.DamageAndDiseaseId} Location: {dmg.forestArea.Location ?? "Unknown"}")
            .ToList();

            options.Add("Exit");

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select a report")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(options)
            );

            var selected = damageAndDisease.Data
            .FirstOrDefault(r => $"Report id: {r.DamageAndDiseaseId} Location: {r.forestArea?.Location ?? "Unknown"}" == selectedOption);

            if (selected != null)
            {
                var fields = new List<string>
                {
                    "ForestAreaId",
                    "TreeId",
                    "SpeciesId",
                    "DamageAndDiseaseType",
                    "Symptom",
                    "Severity",
                    "Reason",
                    "Spread",
                    "DateFirstObservation",
                    "DateLastObservation",
                    "Note",
                    "Exit"
                };

                var selectedField = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select a field to update")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                        .AddChoices(fields)
                );

                switch (selectedField)
                {
                    case "ForestAreaId":
                        Console.WriteLine($"Past info: {selected.ForestAreaId} ({selected.forestArea.Location})");
                        var forestAreas = _forestAreaService.GetAllForestAreas();

                        if (forestAreas.Success)
                        {
                            var areaLocations = forestAreas.Data
                            .Select(area => $"Id:{area.ForestAreaId} - {area.Location}")
                            .ToList();

                            var selectedLocation = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Choose a Forest Area Location")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                                .AddChoices(areaLocations)
                            );

                            var update = forestAreas.Data
                            .FirstOrDefault(u => u.Location == selectedLocation);

                            selected.ForestAreaId = update.ForestAreaId;

                            var result = _damageAndDiseaseService.UpdateDamageAndDisease(selected);

                            if (result.Success)
                            {
                                Console.WriteLine("Forest area updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"Failed to update forest area: {result.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine(forestAreas.Message);
                        }
                        break;

                    case "TreeId":
                        Console.WriteLine($"Past info: {selected.SpeciesId}");
                        break;

                    case "DamageAndDiseaseType":
                        Console.WriteLine($"Past info: {selected.DamageAndDiseaseType ?? "No past info"}");
                        string newType = Utilities.GetString("Enter new info: ", "Try again!");

                        selected.DamageAndDiseaseType = newType;

                        var resultType = _damageAndDiseaseService.UpdateDamageAndDisease(selected);

                        if (resultType.Success)
                        {
                            Console.WriteLine("Type updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to update Type: {resultType.Message}");
                        }
                        break;

                    case "Symptom":
                        Console.WriteLine($"Past info: {selected.Symptom ?? "No past info"}");
                        string newSymptom = Utilities.GetString("Enter new info: ", "Try again!");

                        selected.Symptom = newSymptom;

                        var resultSymptom = _damageAndDiseaseService.UpdateDamageAndDisease(selected);

                        if (resultSymptom.Success)
                        {
                            Console.WriteLine("Symptom updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to update symptom: {resultSymptom.Message}");
                        }
                        break;

                    case "Severity":
                        Console.WriteLine($"Past info: {selected.Severity ?? "No past info"}");
                        string newSeverity = Utilities.GetString("Enter new info: ", "Try again!");

                        selected.Severity = newSeverity;

                        var resultSeverity = _damageAndDiseaseService.UpdateDamageAndDisease(selected);
                        if (resultSeverity.Success)
                        {
                            Console.WriteLine("Severity updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to update severity: {resultSeverity.Message}");
                        }
                        break;

                    case "Reason":
                        Console.WriteLine($"Past info: {selected.Reason ?? "No past info"}");
                        string newReason = Utilities.GetString("Enter new info: ", "Try again!");

                        selected.Reason = newReason;

                        var resultReason = _damageAndDiseaseService.UpdateDamageAndDisease(selected);

                        if (resultReason.Success)
                        {
                            Console.WriteLine("Date updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to update Date: {resultReason.Message}");
                        }
                        break;

                    case "Spread":
                        Console.WriteLine($"Past info: {selected.Spread ?? "No past info"}");
                        string newSpread = Utilities.GetString("Enter new info: ", "Try again!");

                        selected.Spread = newSpread;

                        var resultSpread = _damageAndDiseaseService.UpdateDamageAndDisease(selected);

                        if (resultSpread.Success)
                        {
                            Console.WriteLine("Spread updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to update spread: {resultSpread.Message}");
                        }
                        break;

                    case "DateFirstObservation":
                        Console.WriteLine($"Past info: {selected.DateFirstObservation?.ToString("yyyy-MM-dd")}");
                        DateTime newFirstObservation = Utilities.GetValidDate("Enter new date yyyy-MM-dd: ");

                        selected.DateFirstObservation = newFirstObservation;

                        var resultFirsObservation = _damageAndDiseaseService.UpdateDamageAndDisease(selected);

                        if (resultFirsObservation.Success)
                        {
                            Console.WriteLine("report updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to update report: {resultFirsObservation.Message}");
                        }
                        break;

                    case "DateLastObservation":
                        Console.WriteLine($"Past info: {selected.DateLastObservation?.ToString("yyyy-MM-dd")}");
                        DateTime newLastObservation = Utilities.GetValidDate("Enter new date yyyy-MM-dd: ");

                        selected.DateLastObservation = newLastObservation;

                        var resultLastObservation = _damageAndDiseaseService.UpdateDamageAndDisease(selected);

                        if (resultLastObservation.Success)
                        {
                            Console.WriteLine("report updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to update report: {resultLastObservation.Message}");
                        }
                        break;

                    case "Note":
                        Console.WriteLine($"Past info: {selected.Note ?? "No note Written"}");
                        string newNote = Utilities.GetString("Enter new note: ", "Try again!");

                        selected.Note = newNote;

                        var resultNote = _damageAndDiseaseService.UpdateDamageAndDisease(selected);

                        if (resultNote.Success)
                        {
                            Console.WriteLine("Note Updated succesfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to update note {resultNote.Message}");
                        }
                        break;

                    case "Exit":
                        Console.WriteLine("Exiting update menu.");
                        break;

                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No valid report was selected!");
            }
        }
    }

}
