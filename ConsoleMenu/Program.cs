// See https://aka.ms/new-console-template for more information

using WXNUtils;
using Console = WXNUtils.Console;
using VanillaConsole = System.Console;
//Start
Setup();

static void Setup(){
    
    Console.Utils.DisableCursor();
    VanillaConsole.Title = "ConsoleMenu";
    Console.Utils.SetupConsoleKeepOpen();
    GuiHandleWorkers.SetupBackgroundWorkers();
    Main();
    Console.Utils.KeepConsoleOpen();
}

static void Main()
{
    GuiHandleWorkers.CurrentMenu = new GUI.CmdMenu("Main Menu",
        new List<GUI.MenuItem>()
        {
            new GUI.MenuItem("Test Item 1", () =>
            {
                VanillaConsole.Title = "Called from Item 1";
            }),
            new GUI.MenuItem("Test Item 2", () =>
            {
                GUI.CmdMenu cache = GuiHandleWorkers.CurrentMenu;
                GuiHandleWorkers.CurrentMenu = new GUI.CmdMenu("Test Sub menu", new List<GUI.MenuItem>()
                {
                    new GUI.MenuItem("Test Sub Item 1", () =>
                    {
                        VanillaConsole.Title = "Called from Sub Menu Button 1";
                    }),
                    new GUI.MenuItem("Test Sub Item 2", () =>
                    {
                        VanillaConsole.Title = "Called from Sub Menu Button 2";
                    }),
                    new GUI.MenuItem("Back", () =>
                    {
                        GuiHandleWorkers.CurrentMenu = cache;
                    })
                });
            }),
            new GUI.MenuItem("Test Item 3", () =>
            {
                VanillaConsole.Title = "Called from Item 3";
            })
        });
}
