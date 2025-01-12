using ForestProgram.Models;
using ForestProgram.Services;
using Humanizer;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        "Add Repair Report",
        "View All Damage Repairs",
        "View Specific Repair Report",
        "Update Repair Report",
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
            case "Add Repair Report":
                AddDamageRepair();
                break;
            case "View All Damage Repairs":
                ViewAllDamageRepairs();
                break;
            case "View Specific Repair Report":
                ViewSpecificDamageRepair();
                break;
            case "Update Repair Report":
                UpdateDamageRepair();
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
        .FirstOrDefault(s => $"Report id: {s.DamageAndDiseaseId} - Location: {s.forestArea.Location} - Type: {s.DamageAndDiseaseType}" == selectedReport);

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
        newRepair.damageAndDisease = handleReport;

        var result = _damageRepairService.AddDamageRepair(newRepair);

        if (!result.Success)
        {
            Console.WriteLine(result.Message);
        }
        else
        {
            Console.WriteLine(result.Message);
        }
    }

    private void ViewAllDamageRepairs()
    {
        var repairList = _damageRepairService.GetAllRepairReports();

        foreach (var report in repairList.Data)
        {
            Console.WriteLine("=== Damage Repair Report ===");
            Console.WriteLine($"Report ID: {report.DamageRepairId}");
            Console.WriteLine($"Associated Damage Report ID: {report.DamageAndDiseaseId}");
            Console.WriteLine($"Action: {report.Action ?? "No action specified"}");
            Console.WriteLine($"Responsible: {report.Responsible ?? "Not assigned"}");
            Console.WriteLine($"Time Span: {report.TimeSpan ?? "Not specified"}");
            Console.WriteLine($"Resources Needed: {report.Resources ?? "No resources specified"}");
            Console.WriteLine($"Priority: {report.Priority ?? "Not set"}");
            Console.WriteLine($"Status: {report.Satus ?? "No status specified"}");
            Console.WriteLine($"Follow-Up: {report.FollowUp ?? "No follow-up details"}");
            Console.WriteLine($"Result: {report.Result ?? "No result specified"}");

            if (report.damageAndDisease != null)
            {
                Console.WriteLine("=== Associated Damage And Disease Details ===");
                Console.WriteLine($"Damage/Disease Type: {report.damageAndDisease.DamageAndDiseaseType}");
                Console.WriteLine($"Location: {report.damageAndDisease.forestArea?.Location ?? "Location not specified"}");
                Console.WriteLine($"Description: {report.damageAndDisease.DamageAndDiseaseType ?? "No description of type"}");
            }
            else
            {
                Console.WriteLine("No associated Damage and Disease report found.");
            }

            Console.WriteLine("-------------------------------------------");
        }
    }

    private void ViewSpecificDamageRepair()
    {
        int reportId = Utilities.GetValidIntInput("Enter report id: ", "Try again!");

        var report = _damageRepairService.GetSpecifikRepairReportById(reportId);

        if (!report.Success)
        {
            Console.WriteLine(report.Message);
        }
        else
        {
            Console.WriteLine("=== Damage Repair Report ===");
            Console.WriteLine($"Report ID: {report.Data.DamageRepairId}");
            Console.WriteLine($"Associated Damage Report ID: {report.Data.DamageAndDiseaseId}");
            Console.WriteLine($"Action: {report.Data.Action ?? "No action specified"}");
            Console.WriteLine($"Responsible: {report.Data.Responsible ?? "Not assigned"}");
            Console.WriteLine($"Time Span: {report.Data.TimeSpan ?? "Not specified"}");
            Console.WriteLine($"Resources Needed: {report.Data.Resources ?? "No resources specified"}");
            Console.WriteLine($"Priority: {report.Data.Priority ?? "Not set"}");
            Console.WriteLine($"Status: {report.Data.Satus ?? "No status specified"}");
            Console.WriteLine($"Follow-Up: {report.Data.FollowUp ?? "No follow-up details"}");
            Console.WriteLine($"Result: {report.Data.Result ?? "No result specified"}");

            if (report.Data.damageAndDisease != null)
            {
                Console.WriteLine("=== Associated Damage And Disease Details ===");
                Console.WriteLine($"Damage/Disease Type: {report.Data.damageAndDisease.DamageAndDiseaseType}");
                Console.WriteLine($"Location: {report.Data.damageAndDisease.forestArea?.Location ?? "Location not specified"}");
                Console.WriteLine($"Description: {report.Data.damageAndDisease.DamageAndDiseaseType ?? "No description of type"}");
            }
            else
            {
                Console.WriteLine("No associated Damage and Disease report found.");
            }

            Console.WriteLine("-------------------------------------------");
        }
    }

    private void UpdateDamageRepair()
    {
        AnsiConsole.MarkupLine("[cyan]Update Damage Repair functionality goes here.[/]");
    }

}