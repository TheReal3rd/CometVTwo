using System.Collections.Generic;
using CometVTwo.menu;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.Other
{
    public class ClickMenu : Module
    {
        //Vars
        public static List<WindowElement> windowList = new List<WindowElement>();
        private int windowIDCounter = 1;
        //Settings
        private readonly rectSetting playerWindow = new rectSetting("Player", new Rect(20, 50, 360, 400), ModuleManager.Category.Player);
        private readonly rectSetting movementWindow = new rectSetting("Movement", new Rect(380, 50, 360, 400), ModuleManager.Category.Movement);
        private readonly rectSetting serverWindow = new rectSetting("Server", new Rect(740, 50, 360, 400), ModuleManager.Category.Server);
        private readonly rectSetting otherWindow = new rectSetting("Other", new Rect(1100, 50, 360, 400), ModuleManager.Category.Other);

        private readonly colorSetting playerColour = new colorSetting("PlayerColour", Color.magenta);
        private readonly colorSetting movementColour = new colorSetting("MovementColour", Color.magenta);
        private readonly colorSetting serverColour = new colorSetting("ServerColour", Color.magenta);
        private readonly colorSetting otherColour = new colorSetting("OtherColour", Color.magenta);
        private readonly colorSetting buttonColour = new colorSetting("ButtonColour", Color.magenta);

        public ClickMenu()
        {
            base.SetUp("ClickMenu", ModuleManager.Category.Other, KeyCode.Insert);
            //Rects
            this.moduleSettings.Add(playerWindow);
            this.moduleSettings.Add(movementWindow);
            this.moduleSettings.Add(serverWindow);
            this.moduleSettings.Add(otherWindow);
            //Colour
            this.moduleSettings.Add(playerColour);
            this.moduleSettings.Add(movementColour);
            this.moduleSettings.Add(serverColour);
            this.moduleSettings.Add(otherColour);
            this.moduleSettings.Add(buttonColour);
            //Windows
            windowList.Add(new WindowElement(ModuleManager.Category.Player, Color.magenta, new Rect(20, 50, 360, 400)));
            windowList.Add(new WindowElement(ModuleManager.Category.Movement, Color.magenta, new Rect(380, 50, 360, 400)));
            windowList.Add(new WindowElement(ModuleManager.Category.Server, Color.magenta, new Rect(740, 50, 360, 400)));
            windowList.Add(new WindowElement(ModuleManager.Category.Other, Color.magenta, new Rect(1100, 50, 360, 400)));
        }

        public override void OnGUI()
        {
            windowIDCounter = 1;
            foreach (WindowElement window in windowList)
            {
                GUI.color = window.GetColour();
                window.SetButtonColour(buttonColour.GetValue());
                window.SetWindowRect(GUI.Window(windowIDCounter, window.GetWindowRect(), new GUI.WindowFunction(window.Draw), window.GetCategory().ToString()));
                switch (window.GetCategory())
                {
                    case ModuleManager.Category.Player:
                        playerWindow.SetValue(window.GetWindowRect());
                        window.SetColour(playerColour.GetValue());
                        break;
                    case ModuleManager.Category.Movement:
                        movementWindow.SetValue(window.GetWindowRect());
                        window.SetColour(movementColour.GetValue());
                        break;
                    case ModuleManager.Category.Server:
                        serverWindow.SetValue(window.GetWindowRect());
                        window.SetColour(serverColour.GetValue());
                        break;
                    case ModuleManager.Category.Other:
                        otherWindow.SetValue(window.GetWindowRect());
                        window.SetColour(otherColour.GetValue());
                        break;
                    case ModuleManager.Category.Hidden://Do nothing wow!
                        break;
                }
                windowIDCounter++;
            }
        }
    }
}