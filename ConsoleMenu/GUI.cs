namespace WXNUtils;

public class GUI
{
    public class MenuItem
    {
        public string displayText { get; set; }
        public Action onClick { get; set; }

        public MenuItem(string displayText, Action method)
        {
            this.displayText = displayText;
            this.onClick = method;
        }
    }
    public class CmdMenu
    {
        public int selectedIndex { 
            get; 
            set; 
        }
        public string Name { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public CmdMenu(string Name, List<MenuItem> MenuItems)
        {
            this.MenuItems = MenuItems;
            this.Name = Name;
            this.selectedIndex = 0;
        }
        
    }
}