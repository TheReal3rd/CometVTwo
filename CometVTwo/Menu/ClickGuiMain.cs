using System.Collections.Generic;
using CometVTwo.Modules;
using UnityEngine;

namespace CometVTwo.menu
{
    public class ClickGuiMain
    {
        public bool MenuOpen;
        public List<WindowElement> windowList = new List<WindowElement>();
        private int windowIDCounter = 1;
        //TODO add window position saving.
        public void Init()
        {
            windowList.Add(new WindowElement(ModuleManager.Category.Player, Color.magenta, new Rect(20, 50, 360, 400)));
            windowList.Add(new WindowElement(ModuleManager.Category.Movement, Color.magenta, new Rect(380, 50, 360, 400)));
            windowList.Add(new WindowElement(ModuleManager.Category.Server, Color.magenta, new Rect(740, 50, 360, 400)));
            windowList.Add(new WindowElement(ModuleManager.Category.Other, Color.magenta, new Rect(1100, 50, 360, 400)));
        }
        
        public void OnGUI()
        {
            windowIDCounter = 1;
            foreach (var window in windowList)
            {
                GUI.color = window.GetColour();
                window.SetWindowRect(GUI.Window(windowIDCounter, window.GetWindowRect(), new GUI.WindowFunction(window.Draw), window.getCategory().ToString()));
                windowIDCounter++;
            }
        }

        public void Toggle()
        {
            MenuOpen = !MenuOpen;
        }
    }
}