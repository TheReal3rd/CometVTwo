using CometVTwo.menu;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.MainMenu
{
    public class ClickMenuMainMenu : Module
    {
        //Vars
        public static WindowElement window = new WindowElement(ModuleManager.Category.MainMenu, Color.magenta, new Rect(20, 50, 360, 400));
        
        //Settings
        private readonly static rectSetting mainMenuWindow = new rectSetting("MainMenuWindow", new Rect(20, 50, 360, 400));
        
        public readonly static colorSetting windowColour = new colorSetting("WindowColour", Color.magenta);
        private readonly static colorSetting buttonColour = new colorSetting("ButtonColour", Color.magenta);
        
        public ClickMenuMainMenu()
        {
            base.SetUp("ClickMenuMainMenu", ModuleManager.Category.MainMenu, KeyCode.PageUp);
            this.moduleSettings.Add(mainMenuWindow);
            this.moduleSettings.Add(windowColour);
            this.moduleSettings.Add(buttonColour);
        }

        public override void OnGUI()
        {
            if (mainMenuWindow.Update)
            {
                window.WindowRect = mainMenuWindow.GetValue();
                mainMenuWindow.Update = false;
            }
            GUI.color = windowColour.GetValue();
            window.ButtonColour = buttonColour.GetValue();
            window.WindowRect = GUI.Window(1, window.WindowRect, new GUI.WindowFunction(window.Draw), "ModuleMenu");
            mainMenuWindow.SetValue(window.WindowRect);
        }
    }
}