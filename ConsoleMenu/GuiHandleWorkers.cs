using System.ComponentModel;
using System.Threading.Channels;

namespace WXNUtils;

public class GuiHandleWorkers
{
    public static GUI.CmdMenu CurrentMenu = null;
    public static int WorkerDelay = 200;
    public static BackgroundWorker InputWorker = new BackgroundWorker();
    public static BackgroundWorker RenderWorker = new BackgroundWorker();

    public static void SetupBackgroundWorkers()
    {
        InputWorker.DoWork += InputWorkerOnDoWork;
        RenderWorker.DoWork += RenderWorkerOnDoWork;
        InputWorker.RunWorkerAsync();
        RenderWorker.RunWorkerAsync();
    }

    private static void RenderWorkerOnDoWork(object? sender, DoWorkEventArgs e)
    {
        while (true)
        {
            System.Console.CursorTop = 0;
            System.Console.CursorLeft = 0;
            if(CurrentMenu != null)
                drawMenu(CurrentMenu);
            Thread.Sleep(WorkerDelay);
            System.Console.Clear();
        }
    }

    private static void drawMenu(GUI.CmdMenu menu)
    {
        foreach (GUI.MenuItem menuItem in menu.MenuItems)
        {
            if (menu.MenuItems.IndexOf(menuItem) == menu.selectedIndex)
            {
                Console.WriteLine("> " + menuItem.displayText);
            }
            else
            {
                    
                Console.WriteLine("  " + menuItem.displayText);
            }
        }
    }

    private static void InputWorkerOnDoWork(object? sender, DoWorkEventArgs e)
    {
        while (true)
        {
            ConsoleKey pressedKey = System.Console.ReadKey().Key;
            if (pressedKey == ConsoleKey.UpArrow)
            {
                if (!(CurrentMenu.selectedIndex - 1 < 0))
                {
                    CurrentMenu.selectedIndex = CurrentMenu.selectedIndex-1;
                }
                else
                {
                    CurrentMenu.selectedIndex = CurrentMenu.MenuItems.Count - 1;
                }
                //debugWrite("Menu up");
            }

            if (pressedKey == ConsoleKey.DownArrow)
            {
                //debugWrite("Menu down");
                if (!(CurrentMenu.selectedIndex + 1 > CurrentMenu.MenuItems.Count-1))
                {
                    CurrentMenu.selectedIndex = CurrentMenu.selectedIndex+1;
                }
                else
                {
                    CurrentMenu.selectedIndex = 0;
                }
            }

            if (pressedKey == ConsoleKey.Enter)
            {
                CurrentMenu.MenuItems[CurrentMenu.selectedIndex].onClick.Invoke();
            }
            Thread.Sleep(WorkerDelay);
        }
    }
}