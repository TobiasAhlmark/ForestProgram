
using ForestProgram.Models;
using ForestProgram.UI;
using Microsoft.Extensions.DependencyInjection;
using ForestProgram.Services;

internal class Program
{
    private static void Main()
    {
        // Skapa en ny ServiceCollection
        var serviceCollection = new ServiceCollection();

        // Konfigurera tjänster
        serviceCollection.AddSingleton<ForestProgramDbContext>();
        serviceCollection.AddSingleton<SpeciesUI>();
        serviceCollection.AddSingleton<TreeUI>();
        serviceCollection.AddSingleton<EnviromentUI>();
        serviceCollection.AddSingleton<ForestAreaUI>();
        serviceCollection.AddSingleton<PlantingHistoryUI>();
        serviceCollection.AddSingleton<DamageAndDiseaseUI>();
        serviceCollection.AddSingleton<DamageRepairUI>();
        serviceCollection.AddSingleton<TreeManagementUI>();
        serviceCollection.AddSingleton<MainMenu>();
        serviceCollection.AddSingleton<ForestAreaService>();
        serviceCollection.AddSingleton<TreeManagementService>();
        serviceCollection.AddSingleton<TreeService>();
        serviceCollection.AddSingleton<SpeciesService>();
        serviceCollection.AddSingleton<EnviromentService>();
        serviceCollection.AddSingleton<DamageAndDiseaseService>();
        serviceCollection.AddSingleton<DamageRepairService>();

        // Bygg ServiceProvider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Hämta MainMenu från DI-container
        var userInterface = serviceProvider.GetRequiredService<MainMenu>();
        userInterface.MainMenuOptions();
    }
}
