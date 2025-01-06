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
                    _enviromentUI.EnvironmentMenu();
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

            var exitOptions = new List<string>
            {
                "Go back to the Main Menu",
                "Exit the Application"
            };

            var exitSelection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold red]=== Exit Options ===[/]\nSelect an option using [green]arrow keys[/]:")
                    .PageSize(3)
                    .AddChoices(exitOptions)
            );

            switch (exitSelection)
            {
                case "Go back to the Main Menu":
                    continue; // Fortsätt loopen för att gå tillbaka till huvudmenyn
                case "Exit the Application":
                    AnsiConsole.MarkupLine("[bold red]Exiting the application. Goodbye![/]");
                    return; // Avslutar programmet
            }
        }
    }
}