using ForestProgram.Models;

namespace ForestProgram.UI;

public class TreeUI
{
    private readonly ForestProgramDbContext _forestProgramDbContext;

    public TreeUI (ForestProgramDbContext context)
    {
        _forestProgramDbContext = context;
    }

    public void TreeMenu()
    {
        Console.WriteLine("Tree Menu");
        Console.WriteLine("1. Add tree");
        Console.WriteLine("2. Update tree");
        Console.WriteLine("3. Check information about a tree");

        if (int.TryParse(Console.ReadLine(), out int input))
        {
            switch (input)
            {
                case 1:
                    AddTree();
                    break;
                case 2:
                    UpdateTree();
                    break;
                case 3:
                    CheckInfo();
                    break;

                default:
                    Console.WriteLine("Use numbers betwen 1-3");
                    break;
            }
        }
    }

    public void AddTree()
    {

    }

    public void UpdateTree()
    {

    }

    public void CheckInfo()
    {

    }
}