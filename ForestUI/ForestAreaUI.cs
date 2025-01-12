using ForestProgram.Models;
using ForestProgram.Services;
using Spectre.Console;

namespace ForestProgram.UI;

public class ForestAreaUI
{
    private readonly ForestAreaService _forestAreaService;


    public ForestAreaUI(ForestAreaService forestAreaService)
    {
        _forestAreaService = forestAreaService;
    }

    public void ForestAreaMenu()
    {
        var menuOptions = new List<string>
    {
        "Add Forest Area",
        "Get Forest Area Information",
        "Get specifik Forest Area Information",
        "Update forest area",
        "Exit"
    };

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option from the Forest Area Menu")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(menuOptions)
        );

        switch (selectedOption)
        {
            case "Add Forest Area":
                AddForestArea();
                break;
            case "Get Forest Area Information":
                GetInfoForestArea();
                break;
            case "Get specifik Forest Area Information":
                GetSpecifikInfoForestArea();
                break;
            case "Update forest area":
                UpdateForestarea();
                break;
            case "Exit":
                Console.WriteLine("Exiting the Forest Area Menu...");
                break;
            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }

    public void AddForestArea()
    {

        string location = Utilities.GetString("Enter forestarea location: ", "Try again.");
        string type = Utilities.GetString("Enter foresttype: ", "Try again");
        string squaremeter = Utilities.GetString("Enter squaremeter: ", "Try again");
        string eco = Utilities.GetString("Enter EcoSystem: ", "Try again");
        int age = Utilities.GetValidIntInput("Enter age: ", "Try again");

        ForestArea forestArea = new ForestArea
        {
            ForestType = type,
            AreaSquareMeters = squaremeter,
            Age = age,
            Location = location,
            EcoSystem = eco
        };

        var result = _forestAreaService.AddForestArea(forestArea);

        if (result.Success)
        {
            Console.WriteLine(result.Message);
        }
        else
        {
            Console.WriteLine($"Error! {result.Message}");
        }
    }

    public void GetInfoForestArea()
    {
        var forestareas = _forestAreaService.GetForestAreaWithEnviromentAndDamageAndDisease();

        if (forestareas.Success)
        {
            foreach (var forestArea in forestareas.Data)
            {
                // Utskrift av grundläggande skogsarea information
                Console.WriteLine($"=== Forest Area: {forestArea.ForestAreaId} ===");
                Console.WriteLine($"Location: {forestArea.Location}");

                // Utskrift av relaterad miljöinformation (Enviroments)
                if (forestArea.Enviroments.Any())
                {
                    Console.WriteLine("  Environment Information:");
                    foreach (var env in forestArea.Enviroments)
                    {
                        Console.WriteLine($"    Temperature: {env.Temperature}°C, " +
                                          $"Precipitation: {env.Precipitation} mm, " +
                                          $"Wind: {env.Wind} m/s, " +
                                          $"Altitude: {env.Altitude} m");
                    }
                }
                else
                {
                    Console.WriteLine("  No environment data available.");
                }

                // Utskrift av relaterade skade- och sjukdomsrapporter (DamageAndDiseases)
                if (forestArea.DamageAndDiseases.Any())
                {
                    Console.WriteLine("\n  Damage and Disease Reports:");
                    foreach (var damageAndDisease in forestArea.DamageAndDiseases)
                    {
                        Console.WriteLine($"    Damage and Disease ID: {damageAndDisease.DamageAndDiseaseId}, " +
                                          $"Type: {damageAndDisease.DamageAndDiseaseType}, " +
                                          $"Severity: {damageAndDisease.Severity}, " +
                                          $"Symptom: {damageAndDisease.Symptom}");
                    }
                }
                else
                {
                    Console.WriteLine("  No damage and disease reports available.");
                }

                // Utskrift av relaterade reparationsrapporter (RepairReports)
                if (forestArea.RepairReports.Any())
                {
                    Console.WriteLine("\n  Repair Reports:");
                    foreach (var repairReport in forestArea.RepairReports)
                    {
                        Console.WriteLine($"    Repair Report ID: {repairReport.DamageRepairId}, " +
                                          $"Action: {repairReport.Action}, " +
                                          $"Responsible: {repairReport.Responsible}, " +
                                          $"Priority: {repairReport.Priority}, " +
                                          $"Status: {repairReport.Satus}");
                    }
                }
                else
                {
                    Console.WriteLine("  No repair reports available.");
                }

                Console.WriteLine("\n-----------------------------------------------");
            }
        }
        else
        {
            Console.WriteLine("Could not retrieve forest area information.");
        }
    }

    public void GetSpecifikInfoForestArea()
    {
        var forestArea = _forestAreaService.GetForestAreaWithEnviromentAndDamageAndDisease();

        if (forestArea.Success)
        {
            var forestAreaOptions = forestArea.Data
            .Select(f => $"ID: {f.ForestAreaId} - {f.Location}")
            .ToList();

            forestAreaOptions.Add("Exit");

            var selectedForestArea = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a forest area to view details")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                    .AddChoices(forestAreaOptions)
            );

            if (selectedForestArea.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting the program...");
                return;
            }

            var selectedForestAreaData = forestArea.Data
            .FirstOrDefault(fa => $"ID: {fa.ForestAreaId} - {fa.Location}" == selectedForestArea);

            if (selectedForestAreaData != null)
            {
                Console.WriteLine($"Forest Area: {selectedForestAreaData.ForestAreaId} - {selectedForestAreaData.Location}");

                if (selectedForestAreaData.Enviroments.Any() || selectedForestAreaData.DamageAndDiseases.Any())
                {
                    foreach (var env in selectedForestAreaData.Enviroments)
                    {
                        Console.WriteLine($"  Temperature: {env.Temperature}, Precipitation: {env.Precipitation}, Wind: {env.Wind}, Altitude: {env.Altitude}m");
                    }
                    foreach (var dmg in selectedForestAreaData.DamageAndDiseases)
                    {
                        Console.WriteLine($"   Damage type: {dmg.DamageAndDiseaseType}, Symptom: {dmg.Symptom}, First observationdate {dmg.DateFirstObservation}");
                    }
                }
                else
                {
                    Console.WriteLine("  No environment data available.");
                }
            }
        }
        else
        {
            Console.WriteLine("No forest areas were found!");
        }
    }

    public void UpdateForestarea()
    {
        var selectForestArea = _forestAreaService.GetAllForestAreas();

        if (selectForestArea.Success)
        {
            var forestAreaOption = selectForestArea.Data
            .Select(f => f.Location)
            .ToList();

            forestAreaOption.Add("Exit");

            var selectedForest = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a species to update")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(forestAreaOption)
        );

            if (selectedForest.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting the program...");
                return;
            }

            var handleSelectedForest = selectForestArea.Data
            .FirstOrDefault(f => f.Location == selectedForest);

            HandleUpdateForestArea(handleSelectedForest);

        }
        else
        {
            Console.WriteLine("No forest areas found!");
        }
    }

    public void HandleUpdateForestArea(ForestArea forestArea)
    {
        if (forestArea == null)
        {
            Console.WriteLine("Forest not found!");
        }

        var updateOptions = new List<string>
        {
            "Forest type",
            "Square meters",
            "Age",
            "Location",
            "Ecosystem",
            "Exit"
        };

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select what you want to update")
                .PageSize(5)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(updateOptions)
        );

        switch (selectedOption)
        {
            case "Forest type":
                Console.WriteLine($"Past info {forestArea.ForestType}");
                string newForestType = Utilities.GetString("Enter new forest type: ", "Try again!");
                forestArea.ForestType = newForestType;
                break;

            case "Square meters":
                Console.WriteLine($"Past info {forestArea.AreaSquareMeters}");
                string newSquareMeters = Utilities.GetString("Enter new square meters: ", "Try again!");
                forestArea.AreaSquareMeters = newSquareMeters;
                break;

            case "Age":
                Console.WriteLine($"Past info {forestArea.Age}");
                int newAge = Utilities.GetValidIntInput("Enter new age: ", "Try again!");
                forestArea.Age = newAge;
                break;

            case "Location":
                Console.WriteLine($"Past info {forestArea.Location}");
                string newLocation = Utilities.GetString("Enter new location: ", "Try again!");
                forestArea.Location = newLocation;
                break;

            case "Ecosystem":
                Console.WriteLine($"Past info {forestArea.Location}");
                string newEcosystem = Utilities.GetString("Enter new ecosystem: ", "Try again!");
                forestArea.EcoSystem = newEcosystem;
                break;

            case "Exit":
                Console.WriteLine("Exiting update menu...");
                break;

            default:
                Console.WriteLine("Invalid selection. No changes made.");
                break;
        }

        var infoUpdate = _forestAreaService.UpdateForestArea(forestArea);

        if (infoUpdate.Success)
        {
            Console.WriteLine(infoUpdate.Message);
        }
        else
        {
            Console.WriteLine(infoUpdate.Message);
        }
    }
}