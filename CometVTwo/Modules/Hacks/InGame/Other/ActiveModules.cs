using System;
using System.Collections.Generic;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Other
{
    public class ActiveModules : Module
    {
        //Vars
        private Rect windowRect = new Rect(20, 420, 200, 400);
        private List<String> activeModules = new List<String>();

        //Settings
        private readonly rectSetting activeRect = new rectSetting("Active", new Rect(20, 420, 200, 400));
        
        public ActiveModules()
        {
            base.SetUp("ActiveModules", ModuleManager.Category.Other);
            this.moduleSettings.Add(activeRect);
        }

        public override void OnGUI()
        {
            GUI.color = ClickMenu.otherColour.Value;
            windowRect = GUI.Window(7, windowRect, new GUI.WindowFunction(DrawWindow), "ActiveModules");
            if (activeRect.Update)
            {
                windowRect = activeRect.Value;
                activeRect.Update = false;
            }
            else
            {
                activeRect.Value = windowRect;
            }
        }

        public override void OnUpdate()
        {
            activeModules.Clear();
            foreach (Module module in Main.ModuleManager.modulesList)
            {
                if (module.enabled.Value)
                {
                    activeModules.Add(module.getName());
                }
            }

            windowRect.height = GenerateHeight();
        }

        public override void OnDisable()
        {
            activeModules.Clear();
        }

        private void DrawWindow(int windowID)
        {
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            foreach (var name in activeModules)
            {
                GUILayout.Label(name, new GUILayoutOption[0]);
            }
            GUILayout.EndVertical();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        } 
        
        private int GenerateHeight()
        {
            return (activeModules.Count * 25) + 16;
        }
    }
}