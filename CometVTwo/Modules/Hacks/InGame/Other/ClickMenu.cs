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
        private readonly rectSetting playerWindow = new rectSetting("Player", new Rect(20, 50, 360, 400));
        private readonly rectSetting movementWindow = new rectSetting("Movement", new Rect(380, 50, 360, 400));
        private readonly rectSetting serverWindow = new rectSetting("Server", new Rect(740, 50, 360, 400));
        private readonly rectSetting otherWindow = new rectSetting("Other", new Rect(1100, 50, 360, 400));

        private readonly colorSetting playerColour = new colorSetting("PlayerColour", Color.magenta);
        private readonly colorSetting movementColour = new colorSetting("MovementColour", Color.magenta);
        public static readonly colorSetting serverColour = new colorSetting("ServerColour", Color.magenta);
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
                GUI.color = window.colour;
                window.ButtonColour = buttonColour.GetValue();
                window.WindowRect = GUI.Window(windowIDCounter, window.WindowRect, new GUI.WindowFunction(window.Draw), window.category.ToString());
                switch (window.category)
                {
                    case ModuleManager.Category.Player:
                        if (playerWindow.Update)
                        {
                            window.WindowRect = playerWindow.GetValue();
                            playerWindow.Update = false;
                        }
                        else
                        {
                            playerWindow.SetValue(window.WindowRect);
                            window.colour = playerColour.GetValue();
                        }
                        break;
                    case ModuleManager.Category.Movement:
                        if (movementWindow.Update)
                        {
                            window.WindowRect = movementWindow.GetValue();
                            movementWindow.Update = false;
                        }
                        else
                        {
                            movementWindow.SetValue(window.WindowRect);
                            window.colour = movementColour.GetValue();
                        }
                        break;
                    case ModuleManager.Category.Server:
                        if (serverWindow.Update)
                        {
                            window.WindowRect = serverWindow.GetValue();
                            serverWindow.Update = false;
                        }
                        else
                        {
                            serverWindow.SetValue(window.WindowRect);
                            window.colour = serverColour.GetValue();
                        }
                        break;
                    case ModuleManager.Category.Other:
                        if (otherWindow.Update)
                        {
                            window.WindowRect = otherWindow.GetValue();
                            otherWindow.Update = false;
                        }
                        else
                        {
                            otherWindow.SetValue(window.WindowRect);
                            window.colour = otherColour.GetValue();
                        }
                        break;
                    case ModuleManager.Category.Hidden://Do nothing wow!
                        break;
                }
                windowIDCounter++;
            }
        }
    }
}