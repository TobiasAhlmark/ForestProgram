using ForestProgram.Models;
using ForestProgram.Services;
using Spectre.Console;

namespace ForestProgram.UI;

public class DamageRepairUI
{
    private readonly DamageAndDiseaseService _damageAndDiseaseService;
    private readonly DamageRepairService _damageRepairService;

    public DamageRepairUI
    (
        DamageAndDiseaseService damageAndDiseaseService,
        DamageRepairService damageRepairService
    )
    {
        _damageAndDiseaseService = damageAndDiseaseService;
        _damageRepairService = damageRepairService;
    }

    public void DamageRepairMenu()
    {
        var menuOptions = new List<string>
    {
        "Add Damage Repair",
        "View All Damage Repairs",
        "View Specific Damage Repair",
        "Update Damage Repair",
        "Delete Damage Repair",
        "Exit"
    };

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option from the [green]Damage Repair Menu[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(menuOptions)
        );

        switch (selectedOption)
        {
            case "Add Damage Repair":
                AddDamageRepair();
                break;
            case "View All Damage Repairs":
                ViewAllDamageRepairs();
                break;
            case "View Specific Damage Repair":
                ViewSpecificDamageRepair();
                break;
            case "Update Damage Repair":
                UpdateDamageRepair();
                break;
            case "Delete Damage Repair":
                DeleteDamageRepair();
                break;
            case "Exit":
                Console.WriteLine("Exiting the Damage Repair Menu...");
                break;
            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }

    private void AddDamageRepair()
    {
        var damageAndDiseaseList = _damageAndDiseaseService.GetAllDamageAndDiseaseReports();

        if (!damageAndDiseaseList.Success)
        {
            Console.WriteLine(damageAndDiseaseList.Message);
        }

        var selectReport = damageAndDiseaseList.Data
        .Select(s => $"Report id: {s.DamageAndDiseaseId} - Location: {s.forestArea.Location} - Type: {s.DamageAndDiseaseType}")
        .ToList();

        selectReport.Add("Exit");

        var selectedReport = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Select report")
            .PageSize(10)
            .MoreChoicesText("[grey](Use arrow keys to select)[/]")
            .AddChoices(selectReport)
    );

        if (selectedReport.Equals("Exit", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Exiting the program...");
            return;
        }

        var handleReport = damageAndDiseaseList.Data
        .FirstOrDefault(s => s.DamageAndDiseaseId.ToString() == selectedReport);

        Console.Clear();
        Console.WriteLine("=== Add New Damage Repair ===");

        var newRepair = new DamageRepair
        {
            DamageAndDiseaseId = handleReport.DamageAndDiseaseId,
            Responsible = Utilities.GetString("Enter the person responsible: ", "Try again!"),
            TimeSpan = Utilities.GetString("Enter the expected time span: ", "Try again!"),
            Resources = Utilities.GetString("Enter the resources needed: ", "Try again!"),
            Priority = Utilities.GetString("Enter priority (High/Medium/Low): ", "Try again!"),
            Satus = Utilities.GetString("Enter status (Open/Closed/In Progress): ", "Try again!"),
            FollowUp = Utilities.GetString("Enter follow-up details: ", "Try again!"),
            Result = Utilities.GetString("Enter result (if any): ", "Try again!")
        };

        var result = _damageRepairService.AddDamageRepair(newRepair);
    }

    private void ViewAllDamageRepairs()
    {
        AnsiConsole.MarkupLine("[blue]View All Damage Repairs functionality goes here.[/]");
    }

    private void ViewSpecificDamageRepair()
    {
        AnsiConsole.MarkupLine("[yellow]View Specific Damage Repair functionality goes here.[/]");
    }

    private void UpdateDamageRepair()
    {
        AnsiConsole.MarkupLine("[cyan]Update Damage Repair functionality goes here.[/]");
    }

    private void DeleteDamageRepair()
    {
        AnsiConsole.MarkupLine("[red]Delete Damage Repair functionality goes here.[/]");
    }
}