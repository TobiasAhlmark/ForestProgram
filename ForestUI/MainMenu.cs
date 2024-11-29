using ForestProgram.Models;

namespace ForestProgram.UI;

public class MainMenu
{
    private readonly SpeciesUI _speciesUI;
    private readonly TreeUI _treeUI;
    private readonly TreeManagementUI _treeManagementUI;
    public void MainMenuOptions()
    {
        _speciesUI.SpeciesMenu();
       
    }

}