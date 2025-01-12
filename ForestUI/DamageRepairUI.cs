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
            FollowUp = "",
            Result = ""
        };
        newRepair.DamageAndDisease = handleReport;

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

            if (report.DamageAndDisease != null)
            {
                Console.WriteLine("=== Associated Damage And Disease Details ===");
                Console.WriteLine($"Damage/Disease Type: {report.DamageAndDisease.DamageAndDiseaseType}");
                Console.WriteLine($"Location: {report.DamageAndDisease.forestArea?.Location ?? "Location not specified"}");
                Console.WriteLine($"Description: {report.DamageAndDisease.DamageAndDiseaseType ?? "No description of type"}");
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

            if (report.Data.DamageAndDisease != null)
            {
                Console.WriteLine("=== Associated Damage And Disease Details ===");
                Console.WriteLine($"Damage/Disease Type: {report.Data.DamageAndDisease.DamageAndDiseaseType}");
                Console.WriteLine($"Location: {report.Data.DamageAndDisease.forestArea?.Location ?? "Location not specified"}");
                Console.WriteLine($"Description: {report.Data.DamageAndDisease.DamageAndDiseaseType ?? "No description of type"}");
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
        var damageRepairs = _damageRepairService.GetAllRepairReports();

        if (!damageRepairs.Success)
        {
            Console.WriteLine(damageRepairs.Message);
        }
        else
        {
            var options = damageRepairs.Data
                .Select(dmg => $"Repair ID: {dmg.DamageRepairId} Associated Report ID: {dmg.DamageAndDiseaseId}")
                .ToList();

            options.Add("Exit");

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a damage repair to update")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                    .AddChoices(options)
            );

            if (selectedOption.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting...");
                return;
            }

            var selectedRepair = damageRepairs.Data
                .FirstOrDefault(r => $"Repair ID: {r.DamageRepairId} Associated Report ID: {r.DamageAndDiseaseId}" == selectedOption);

            if (selectedRepair != null)
            {
                var fields = new List<string>
                {
                    "Action",
                    "Responsible",
                    "TimeSpan",
                    "Resources",
                    "Priority",
                    "Satus",
                    "FollowUp",
                    "Result",
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
                    case "Action":
                        Console.WriteLine($"Past info {selectedRepair.Action}");
                        string newAction = Utilities.GetString("Enter new info: ", "Try again!");
                        selectedRepair.Action = newAction;

                        var resultAction = _damageRepairService.UpdateRepairReport(selectedRepair);

                        if(!resultAction.Success)
                        {
                            Console.WriteLine(resultAction.Message);
                        }
                        else
                        {
                            Console.WriteLine($"Repair report ID: {resultAction.Data.DamageRepairId} Updated!");
                        }
                        break;

                    case "Responsible":
                        Console.WriteLine($"Past info {selectedRepair.Responsible}");
                        string newResponsible = Utilities.GetString("Enter new info: ", "Try again!");
                        selectedRepair.Responsible = newResponsible;

                        var resultResponsible = _damageRepairService.UpdateRepairReport(selectedRepair);

                        if(!resultResponsible.Success)
                        {
                            Console.WriteLine(resultResponsible.Message);
                        }
                        else
                        {
                            Console.WriteLine($"Repair report ID: {resultResponsible.Data.DamageRepairId} Updated!");
                        }
                        break;

                    case "TimeSpan":
                        Console.WriteLine($"Past info {selectedRepair.TimeSpan}");
                        string newTimeSpan = Utilities.GetString("Enter new info: ", "Try again!");
                        selectedRepair.TimeSpan = newTimeSpan;

                        var resultTimeSpan = _damageRepairService.UpdateRepairReport(selectedRepair);

                        if(!resultTimeSpan.Success)
                        {
                            Console.WriteLine(resultTimeSpan.Message);
                        }
                        else
                        {
                            Console.WriteLine($"Repair report ID: {resultTimeSpan.Data.DamageRepairId} Updated!");
                        }
                        break;

                    case "Resources":
                        Console.WriteLine($"Past info {selectedRepair.Resources}");
                        string newResources = Utilities.GetString("Enter new info: ", "Try again!");
                        selectedRepair.Resources = newResources;

                        var resultResources = _damageRepairService.UpdateRepairReport(selectedRepair);

                        if(!resultResources.Success)
                        {
                            Console.WriteLine(resultResources.Message);
                        }
                        else
                        {
                            Console.WriteLine($"Repair report ID: {resultResources.Data.DamageRepairId} Updated!");
                        }
                        break;

                    case "Priority":
                        Console.WriteLine($"Past info {selectedRepair.Priority}");
                        string newPriority = Utilities.GetString("Enter new info: ", "Try again!");
                        selectedRepair.Priority = newPriority;

                        var resultPriority = _damageRepairService.UpdateRepairReport(selectedRepair);

                        if(!resultPriority.Success)
                        {
                            Console.WriteLine(resultPriority.Message);
                        }
                        else
                        {
                            Console.WriteLine($"Repair report ID: {resultPriority.Data.DamageRepairId} Updated!");
                        }
                        break;

                    case "Satus":
                        Console.WriteLine($"Past info {selectedRepair.Satus}");
                        string newStatus = Utilities.GetString("Enter new info: ", "Try again!");
                        selectedRepair.Satus = newStatus;

                        var resultStatus = _damageRepairService.UpdateRepairReport(selectedRepair);

                        if(!resultStatus.Success)
                        {
                            Console.WriteLine(resultStatus.Message);
                        }
                        else
                        {
                            Console.WriteLine($"Repair report ID: {resultStatus.Data.DamageRepairId} Updated!");
                        }
                        break;

                    case "FollowUp":
                        Console.WriteLine($"Past info {selectedRepair.FollowUp} - Status: {selectedRepair.Satus}");
                        string newFollowUp = Utilities.GetString("Enter follow up info: ", "Try again!");
                        string status = Utilities.GetString("Enter status (Open/Closed/In Progress): ", "Try again!");
                        selectedRepair.FollowUp = newFollowUp;
                        selectedRepair.Satus = status;
                        selectedRepair.Result = "Follow Up Completed";

                        var resultFollowUp = _damageRepairService.UpdateRepairReport(selectedRepair);

                        if(!resultFollowUp.Success)
                        {
                            Console.WriteLine(resultFollowUp.Message);
                        }
                        else
                        {
                            Console.WriteLine($"Repair report ID: {resultFollowUp.Data.DamageRepairId} Updated!");
                        }
                        break;

                    case "Result":
                        Console.WriteLine($"Past info {selectedRepair.Result}");
                        string newResult = Utilities.GetString("Enter new info: ", "Try again!");
                        selectedRepair.Result = newResult;

                        var resultResult = _damageRepairService.UpdateRepairReport(selectedRepair);

                        if(!resultResult.Success)
                        {
                            Console.WriteLine(resultResult.Message);
                        }
                        else
                        {
                            Console.WriteLine($"Repair report ID: {resultResult.Data.DamageRepairId} Updated!");
                        }
                        break;

                    case "Exit":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option selected.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No matching repair found.");
            }
        }
    }

}