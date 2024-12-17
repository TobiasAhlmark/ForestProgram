using ForestProgram.Models;
using Spectre.Console;

namespace ForestProgram.UI;

public class EnviromentUI
{
    private readonly ForestProgramDbContext _forestProgramContext;

    public EnviromentUI(ForestProgramDbContext forestProgramDbContext)
    {
        _forestProgramContext = forestProgramDbContext;
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
                AddEnvironmentPrompt();
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

}