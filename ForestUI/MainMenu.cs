using ForestProgram.Models;
using Spectre.Console;

namespace ForestProgram.UI;

public class MainMenu
{
    private readonly ForestProgramDbContext _forestProgramContext;
    private readonly SpeciesUI _speciesUI;
    private readonly TreeUI _treeUI;
    private readonly EnviromentUI _enviromentUI;
    private readonly ForestAreaUI _forestAreaUI;
    private readonly PlantingHistoryUI _plantingHistoryUI;
    private readonly DamageAndDiseaseUI _damageAndDiseaseUI;
    private readonly DamageRepairUI _damageRepairUI;
    private readonly TreeManagementUI _treeManagementUI;

    public MainMenu
    (
        ForestProgramDbContext forestProgramDbContext,
        SpeciesUI speciesUI,
        TreeUI treeUI,
        EnviromentUI enviromentUI,
        ForestAreaUI forestAreaUI,
        PlantingHistoryUI plantingHistoryUI,
        DamageAndDiseaseUI damageAndDiseaseUI,
        DamageRepairUI damageRepairUI,
        TreeManagementUI treeManagementUI
    )
    {
        _forestProgramContext = forestProgramDbContext;
        _speciesUI = speciesUI;
        _treeUI = treeUI;
        _enviromentUI = enviromentUI;
        _forestAreaUI = forestAreaUI;
        _plantingHistoryUI = plantingHistoryUI;
        _damageAndDiseaseUI = damageAndDiseaseUI;
        _damageRepairUI = damageRepairUI;
        _treeManagementUI = treeManagementUI;
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
                    .PageSize(10) 
                    .AddChoices(menuItems)
            );

            switch (selection)
            {
                case "Tree Management":
                    _treeManagementUI.ManagementMenu();
                    break;
                case "Environment Settings":
                    _enviromentUI.EnviromentMenu();
                    break;
                case "Species Overview":
                    _speciesUI.SpeciesMenu();
                    break;
                case "Forest Area Analysis":
                    _forestAreaUI.ForestAreaMenu();
                    break;
                case "Tree Inventory":
                    _treeUI.TreeMenu();
                    break;
                case "Planting History":
                    _plantingHistoryUI.PlantingHistoryMenu();
                    break;
                case "Damage and Disease Records":
                    _damageAndDiseaseUI.DamageAndDiseaseMenu();
                    break;
                case "Repair and Maintenance":
                    _damageRepairUI.DamageRepairMenu();
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
    }
}