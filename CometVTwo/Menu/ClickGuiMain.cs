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
        //Temp TODO add drag and resize the window based on the games res.
        private float xOffset;
        public void Init()
        {
            windowList.Add(new WindowElement(ModuleManager.Category.Player, Color.magenta));
            windowList.Add(new WindowElement(ModuleManager.Category.Movement, Color.magenta));
            windowList.Add(new WindowElement(ModuleManager.Category.Server, Color.magenta));
            windowList.Add(new WindowElement(ModuleManager.Category.Other, Color.magenta));
        }
        
        public void OnGUI()
        {
            xOffset = 0;
            windowIDCounter = 1;
            foreach (var window in windowList)
            {
                GUI.color = window.GetColour();
                GUI.Window(windowIDCounter, new Rect(20+xOffset, 50, 360, 400), new GUI.WindowFunction(window.Draw), window.getCategory().ToString());
                xOffset += 365;
                windowIDCounter++;
            }
        }

        public void Toggle()
        {
            MenuOpen = !MenuOpen;
        }
    }
}