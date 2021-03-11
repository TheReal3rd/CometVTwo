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
        private Rect windowRect;
        private Vector2 scrollPosition;
        public WindowElement(ModuleManager.Category category, Color color, Rect windowRect)
        {
            this.Category = category;
            this.Colour = color;
            this.windowRect = windowRect;
        }
        
        public void Draw(int windowID)
        {
            GUI.color = Colour;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(windowRect.width - 15), GUILayout.Height(windowRect.height - 15));
            GUI.color = Color.white;
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
                                            Main.BindingHandler.StartBinding(module);
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
                                        foreach (string selection in select.GetSelection())
                                        {
                                            if (GUILayout.Button(selection, new GUILayoutOption[0]))
                                            {
                                                select.SetSelected(selection);
                                                select.ToggleShowing();
                                            }
                                        }
                                    }
                                    GUI.color = Color.white;
                                    break;
                                case Setting.SettingType.Colour://Sooo the colours are not an enum lol... need to fix.
                                    var colour = (colorSetting) setting;
                                    if (colour.IsChanging())
                                    {
                                        GUI.color = Color.grey;
                                    }
                                    else
                                    {
                                        GUI.color = Color.white;
                                    }
                                    if (!colour.IsChanging())
                                    {
                                        if (GUILayout.Button(String.Format("{0}: {1}", colour.GetName(), colour.GetValue().ToString()), new GUILayoutOption[0]))
                                        {
                                            colour.ToggleChanging();
                                        }
                                    }
                                    else
                                    {
                                        GUI.color = Color.red;
                                        foreach (Color color in System.Enum.GetValues(typeof(Color)))
                                        {
                                            if (GUILayout.Button(color.ToString(), new GUILayoutOption[0]))
                                            {
                                                colour.SetValue(color);
                                                colour.ToggleChanging();
                                            }
                                        }
                                    }
                                    break;
                                case Setting.SettingType.Rect:
                                    //Do nothing.
                                    break;
                            }
                        }
                    }
                    GUILayout.EndVertical();
                }
            }
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        }

        public Color GetColour()
        {
            return this.Colour;
        }

        public Rect GetWindowRect()
        {
            return windowRect;
        }
        public void SetWindowRect(Rect windowRect)
        {
            this.windowRect = windowRect;
        }

        public ModuleManager.Category getCategory()
        {
            return Category;
        }
    }
}