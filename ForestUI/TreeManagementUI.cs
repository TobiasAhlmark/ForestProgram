using ForestProgram.Models;

namespace ForestProgram.UI;

public class TreeManagementUI
{
    private readonly ForestProgramDbContext _forestProgramContext;

    public TreeManagementUI (ForestProgramDbContext context)
    {
        _forestProgramContext = context;
    }
    public void ManagementMenu()
    {
        Console.WriteLine("1. Add management");
        Console.WriteLine("2. Update management");
        Console.WriteLine("3. Show info about specifik management");

        if (int.TryParse(Console.ReadLine(), out int input))
        {
            switch (input)
            {
                case 1:
                    AddManagement();
                    break;
                case 2:
                    UpdateManagement();
                    break;
                case 3:
                    ShowInfoManagement();
                    break;
                
                default:
                    Console.WriteLine("Use numbers betwen 1-3");
                    break;
            }
        }
    }

    public void AddManagement()
    {

    }

    public void UpdateManagement()
    {

    }

    public void ShowInfoManagement()
    {

    }
}