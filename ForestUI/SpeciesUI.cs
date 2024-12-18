using ForestProgram.Models;
using Microsoft.CodeAnalysis;
using Spectre.Console;
using ForestProgram.Services;

namespace ForestProgram.UI;

public class SpeciesUI
{
    private readonly SpeciesService _speciesService;

    public SpeciesUI
    (
        SpeciesService speciesService
    )
    {
        _speciesService = speciesService;
    }
    public void SpeciesMenu()
    {
        var menuOptions = new List<string>
    {
        "Add Species",
        "Update Species",
        "Display Species",
        "Exit"
    };

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option from the Species Menu")
                .PageSize(10)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(menuOptions)
        );

        switch (selectedOption)
        {
            case "Add Species":
                AddSpecies();
                break;
            case "Update Species":
                UpdateSpecies();
                break;
            case "Display Species":
                DisplaySpecies();
                break;
            case "Exit":
                Console.WriteLine("Exiting the Species Menu...");
                break;
            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }

    public void AddSpecies()
    {
        bool menu = true;
        while (menu)
        {
            Species addSpecies = new();
            Console.WriteLine("Add Species");

            string species = Utilities.GetString("Enter species name: ", "Try again!");

            var result = _speciesService.GetSpeciesByName(species);

            if (result.Success)
            {
                Console.WriteLine($"{result.Message}");
                Console.WriteLine("Going back to menu!");
                break;
            }
            else if (!result.Success)
            {
                Console.WriteLine($"{result.Message}");
            }
            string type = Utilities.GetString("coniferous trees or deciduous tree: ", "Try again!");
            string lifeSpan = Utilities.GetString("Average lifespan: ", "Try again!");
            string adaptation = Utilities.GetString("Adaptation, how and where: ", "Try again!");

            addSpecies.Name = species;
            addSpecies.Type = type;
            addSpecies.LifeSpan = lifeSpan;
            addSpecies.Adaptation = adaptation;

            _speciesService.AddSpecies(addSpecies);

            Console.WriteLine($"added!");
            Console.WriteLine("Add new species (1) mainmenu (0)");
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (input == 0)
                {
                    menu = false;
                }
            }
        }
    }

    public void UpdateSpecies()
    {
        var speciesList = _speciesService.GetAllSpecies();

        if (speciesList.Success)
        {
            var speciesOptions = speciesList.Data
            .Select(s => s.Name)
            .ToList();

            speciesOptions.Add("Exit");

            var selectedSpecies = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a species to update")
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

            HandleUpdateSpecies(handleSpecie);

        }
        else
        {
            Console.WriteLine("No species where found!");
        }
    }

    public void HandleUpdateSpecies(Species species)
    {
        if (species == null)
        {
            Console.WriteLine("Species not found!");
            return;
        }

        var updateOptions = new List<string>
    {
        "Name",
        "Type",
        "LifeSpan",
        "Adaptation",
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
            case "Name":
                Console.WriteLine($"Past info {species.Name}");
                string newName = Utilities.GetString("Enter new name: ", "Try again!");
                species.Name = newName;
                break;

            case "Type":
                Console.WriteLine($"Past info {species.Type}");
                string newType = Utilities.GetString("Enter new typ: ", "Try again!");
                species.Type = newType;
                break;

            case "LifeSpan":
                Console.WriteLine($"Past info {species.LifeSpan}");
                string newLifespan = Utilities.GetString("Enter new lifespan: ", "Try again!");
                species.LifeSpan = newLifespan;
                break;

            case "Adaptation":
                Console.WriteLine($"Past info {species.Adaptation}");
                string newAdaptation = Utilities.GetString("Enter new adaptation: ", "Try again!");
                species.Adaptation = newAdaptation;
                break;

            case "Exit":
                Console.WriteLine("Exiting update menu...");
                break;

            default:
                Console.WriteLine("Invalid selection. No changes made.");
                break;
        }
        _speciesService.UpdateSpecies(species);

        
    }

    public void DisplaySpecies()
    {
        var menuOptions = new List<string>
    {
        "Show all species",
        "Search for a specific species",
        "Exit"
    };

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What do you want to do?")
                .PageSize(5)
                .MoreChoicesText("[grey](Use arrow keys to select)[/]")
                .AddChoices(menuOptions)
        );

        switch (selectedOption)
        {
            case "Show all species":
                ShowAllSpecies();
                break;

            case "Search for a specific species":
                SearchForSpecies();
                break;

            case "Exit":
                Console.WriteLine("Exiting the program...");
                return;

            default:
                Console.WriteLine("Invalid selection. Please try again.");
                break;
        }
    }

    public void ShowAllSpecies()
    {
        var speciesList = _speciesService.GetAllSpecies();
        if (!speciesList.Success)
        {
            Console.WriteLine(speciesList.Message);
        }
        Console.WriteLine("\n--- All Species ---");

        Console.WriteLine("{0,-20} {1,-15} {2,-10}", "Name", "Life Span", "Type");
        Console.WriteLine(new string('-', 50)); // Separator

        // Skriv ut varje art i tabellformat
        foreach (var species in speciesList.Data)
        {
            Console.WriteLine(
                "{0,-20} {1,-15} {2,-10}",
                species.Name,               // Justeras till vänster, max 20 tecken
                species.LifeSpan,          // Justeras till vänster, max 15 tecken
                species.Type               // Justeras till vänster, max 10 tecken
            );
        }
    }

    public void SearchForSpecies()
    {
        string searchSpecies = Utilities.GetString("Search for species by name: ", "Try again!");
        var species = _speciesService.GetSpeciesByName(searchSpecies);

        if(!species.Success)
        {
           Console.WriteLine(species.Message);
        }

        Console.WriteLine(species.Data.Name);
        Console.WriteLine(species.Data.LifeSpan);
        Console.WriteLine(species.Data.Type);
        Console.WriteLine(species.Data.Adaptation);
    }

}