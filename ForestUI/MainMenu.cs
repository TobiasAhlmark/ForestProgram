using ForestProgram.Models;
using Spectre.Console;

namespace ForestProgram.UI;

public class MainMenu
{
    private readonly ForestProgramDbContext _forestProgramContext;
    private readonly SpeciesUI _speciesUI;
    private readonly TreeUI _treeUI;
    private readonly TreeManagementUI _treeManagementUI;

    public MainMenu(ForestProgramDbContext forestProgramDbContext)
    {
        _forestProgramContext = forestProgramDbContext;
    }
    public void MainMenuOptions()
    {

        // Menyalternativ
        string[] menuItems = {
            "Tree Management",
            "Environment Settings",
            "Species Overview",
            "Forest Area Analysis",
            "Tree Inventory",
            "Planting History",
            "Damage and Disease Records",
            "Repair and Maintenance"
        };

        while (true)
        {
            // Visa meny med Spectre.Console
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold blue]=== Main Menu ===[/]\nSelect an option using [green]arrow keys[/]:")
                    .PageSize(10) // Max antal val som visas samtidigt
                    .AddChoices(menuItems)
            );

            switch (selection)
            {
                case "Tree Management":
                    _treeManagementUI.ManagementMenu();
                    break;
                case "Environment Settings":
                    EnvironmentSettingsHandler();
                    break;
                case "Species Overview":
                    SpeciesOverviewHandler();
                    break;
                case "Forest Area Analysis":
                    ForestAreaAnalysisHandler();
                    break;
                case "Tree Inventory":
                    _treeUI.TreeMenu();
                    break;
                case "Planting History":
                    PlantingHistoryHandler();
                    break;
                case "Damage and Disease Records":
                    DamageAndDiseaseHandler();
                    break;
                case "Repair and Maintenance":
                    RepairAndMaintenanceHandler();
                    break;
            }

            // Behandla valet
            AnsiConsole.MarkupLine($"[bold yellow]You selected:[/] [green]{selection}[/]");

            // Bekräftelse att fortsätta eller avsluta
            var continueApp = AnsiConsole.Confirm("Would you like to go back to the main menu?");
            if (!continueApp)
            {
                AnsiConsole.MarkupLine("[bold red]Exiting the application. Goodbye![/]");
                break;
            }
        }
        _speciesUI.SpeciesMenu();
    }

}