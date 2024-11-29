
using ForestProgram.Models;
using Microsoft.Extensions.Options;

internal class Program
{
    private static void Main()
    {
        
        ForestProgramDbContext _context = new ForestProgramDbContext();
        MainMenu userInterface = new MainMenu(_context);
        userInterface.MainMenuOptions();
        
    }
}
