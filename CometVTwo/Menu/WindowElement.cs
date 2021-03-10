using System;
using CometVTwo.Modules;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.menu
{
    public class WindowElement
    {
        public ModuleManager.Category Category;
        private Color Colour;
        public WindowElement(ModuleManager.Category category, Color color)
        {
            this.Category = category;
            this.Colour = color;
        }
        
        public void Draw(int windowID)
        {
            var manager = Main.ModuleManager;
            foreach (var module in manager.modulesList)
            {
                if (module.getCategory().Equals(Category))
                {
                    GUILayout.BeginVertical(new GUILayoutOption[0]);
                    GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                    if (module.enabled)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }
                    GUILayout.Label(module.getName(), new GUILayoutOption[0]);
                    GUI.color = Color.magenta;
                    if (GUILayout.Button("Toggle", new GUILayoutOption[0]))
                    {
                        Main.ModuleManager.Toggle(module);
                    }
                    if(module.moduleSettings.Count != 0) {
                        if (GUILayout.Button("Settings", new GUILayoutOption[0]))
                        {
                            Main.ModuleManager.ToggleSettings(module);
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    //Rendering Settings
                    GUILayout.BeginVertical(new GUILayoutOption[0]);
                    if (module.showingSettings)
                    {
                        foreach (var setting in module.moduleSettings)
                        {
                            switch (setting.GetSType())
                            {
                                case Setting.SettingType.Logic:
                                    var boolean = (booleanSetting) setting;
                                    GUI.color = Color.white;
                                    if (boolean.GetValue())
                                    {
                                        GUI.color = Color.green;
                                    }
                                    else
                                    {
                                        GUI.color = Color.white;
                                    }
                                    if (GUILayout.Button(boolean.GetName(), new GUILayoutOption[0]))
                                    {
                                        boolean.Toggle();
                                    }
                                    GUI.color = Color.white;
                                    break;
                                case Setting.SettingType.Numeric:
                                    var numeric = (doubleSetting) setting;
                                    GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                                    GUI.color = Color.white;
                                    GUILayout.Label(String.Format("Name: {0} Value: {1}", numeric.GetName(), numeric.GetValue()), new GUILayoutOption[0]);
                                    if (GUILayout.Button("+", new GUILayoutOption[0]))
                                    {
                                        numeric.Increase();
                                    }
                                    if (GUILayout.Button("-", new GUILayoutOption[0]))
                                    {
                                        numeric.Decrease();
                                    }
                                    GUILayout.EndHorizontal();
                                    GUI.color = Color.white;
                                    break;
                                case Setting.SettingType.Bind:
                                    var bind = (bindSetting) setting;
                                    GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                                    GUI.color = Color.white;
                                    GUILayout.Label(String.Format("Bind: {0}", bind.GetVelue().ToString()), new GUILayoutOption[0]);
                                    if (bind.GetVelue().Equals(KeyCode.None) || bind.GetVelue().Equals(null))
                                    {
                                        if (GUILayout.Button("Set", new GUILayoutOption[0]))
                                        {
                                            //Set code here
                                        }
                                    }
                                    else
                                    {
                                        if (GUILayout.Button("Del", new GUILayoutOption[0]))
                                        {
                                            bind.SetValue(KeyCode.None);
                                        }
                                    }
                                    GUILayout.EndHorizontal();
                                    GUI.color = Color.white;
                                    break;
                                case Setting.SettingType.Enum:
                                    var select = (enumSetting) setting;
                                    if (select.IsShowing())
                                    {
                                        GUI.color = Color.grey;
                                    }
                                    else
                                    {
                                        GUI.color = Color.white;
                                    }

                                    if (!select.IsShowing())
                                    {
                                        if (GUILayout.Button(String.Format("{0}: {1}", select.GetName(), select.GetSelected()), new GUILayoutOption[0]))
                                        {
                                            select.ToggleShowing();
                                        }
                                    }
                                    else
                                    {
                                        GUI.color = Color.red;
                                        for (int z = 0; z != select.GetSelection().Length; z++)
                                        {
                                            if (GUILayout.Button(select.GetSelection()[z], new GUILayoutOption[0]))
                                            {
                                                select.SetSelected(select.GetSelection()[z]);
                                                select.ToggleShowing();
                                            }
                                        }
                                    }
                                    GUI.color = Color.white;
                                    break;
                            }
                        }
                    }
                    GUILayout.EndVertical();
                }
            }
            GUI.DragWindow(new Rect(0,0,1000,1000));
        }

        public Color GetColour()
        {
            return this.Colour;
        }

        public ModuleManager.Category getCategory()
        {
            return Category;
        }
    }
}