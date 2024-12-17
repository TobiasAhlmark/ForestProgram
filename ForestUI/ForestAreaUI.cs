using ForestProgram.Models;
using ForestProgram.Services;

namespace ForestProgram.UI;

public class ForestAreaUI
{
    private readonly ForestAreaService _forestAreaService;

    public ForestAreaUI (ForestAreaService forestAreaService)
    {
        _forestAreaService = forestAreaService;
    }

    public void ForestAreaMenu()
    {
        var forestareas = _forestAreaService.GetForestAreaWithEnviroment();

        if(forestareas.Success)
        {
             foreach (var forestArea in forestareas.Data)
        {
            Console.WriteLine($"Forest Area: {forestArea.ForestAreaId} - {forestArea.Location}");

            if (forestArea.Enviroments.Any())
            {
                foreach (var env in forestArea.Enviroments)
                {
                    Console.WriteLine($"  Temperature: {env.Temperature}, Precipitation: {env.Precipitation}mm, Wind: {env.Wind} m/s, Altitude: {env.Altitude}m");
                }
            }
            else
            {
                Console.WriteLine("  No environment data available.");
            }
        }
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
}