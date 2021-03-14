using System;
using CometVTwo.Modules;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.menu
{
    public class WindowElement
    {
        private ModuleManager.Category Category;
        private Color Colour;
        private Color buttonColour;
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
                    if (module.enabled.Value)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }
                    GUILayout.Label(module.getName(), new GUILayoutOption[0]);
                    GUI.color = buttonColour;
                    if (GUILayout.Button("Toggle", new GUILayoutOption[0]))
                    {
                        Main.ModuleManager.Toggle(module);
                    }
                    if(module.moduleSettings.Count != 0) {
                        if (module.showingSettings)
                        {
                            GUI.color = Color.green;
                        }
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
                            if(!setting.Visible) continue;
                            switch (setting.GetSType())
                            {
                                case Setting.SettingType.Logic:
                                    var boolean = (booleanSetting) setting;
                                    GUI.color = Color.white;
                                    if (boolean.Value)
                                    {
                                        GUI.color = Color.green;
                                    }
                                    else
                                    {
                                        GUI.color = buttonColour;
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
                                    GUI.color = buttonColour;
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
                                    GUILayout.Label(String.Format("Bind: {0}", bind.Bind.ToString()), new GUILayoutOption[0]);
                                    GUI.color = buttonColour;
                                    if (bind.Bind.Equals(KeyCode.None) || bind.Bind.Equals(null))
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
                                            bind.Bind = KeyCode.None;
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
                                        GUI.color = buttonColour;
                                    }
                                    if (!select.IsShowing())
                                    {
                                        if (GUILayout.Button(String.Format("{0}: {1}", select.GetName(), select.Selected), new GUILayoutOption[0]))
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
                                                select.Selected = selection;
                                                select.ToggleShowing();
                                            }
                                        }
                                    }
                                    GUI.color = Color.white;
                                    break;
                                case Setting.SettingType.Colour:
                                    var colour = (colorSetting) setting;
                                    if (!colour.IsChanging())
                                    {
                                        GUI.color = colour.Value;
                                        colour.rgbNew = Utils.Utils.GetRGB(colour.Value);
                                        if (GUILayout.Button(String.Format("Change: {0}", colour.GetName()), new GUILayoutOption[0]))
                                        {
                                            colour.ToggleChanging();
                                        }
                                    }
                                    else
                                    {
                                        if (colour.rgbNew.Length <= 0)
                                        {
                                            colour.rgbNew = Utils.Utils.GetRGB(colour.Value);
                                        }
                                        GUI.color = Utils.Utils.RGBToColour(colour.rgbNew);
                                        GUILayout.Label("Name: "+colour.GetName(), new GUILayoutOption[0]);
                                        GUILayout.Label("Red: "+ colour.rgbNew[0], new GUILayoutOption[0]);
                                        colour.rgbNew[0] = (int)GUILayout.HorizontalSlider(colour.rgbNew[0], 0.0f,255.0f, new GUILayoutOption[0]);
                                        GUILayout.Label("Green: "+ colour.rgbNew[1], new GUILayoutOption[0]);
                                        colour.rgbNew[1] = (int)GUILayout.HorizontalSlider(colour.rgbNew[1], 0.0f,255.0f, new GUILayoutOption[0]);
                                        GUILayout.Label("Blue: "+ colour.rgbNew[2], new GUILayoutOption[0]);
                                        colour.rgbNew[2] = (int)GUILayout.HorizontalSlider(colour.rgbNew[2], 0.0f,255.0f, new GUILayoutOption[0]);
                                        GUI.color = buttonColour;
                                        if (GUILayout.Button("Set", new GUILayoutOption[0]))
                                        {
                                            colour.Value = Utils.Utils.RGBToColour(colour.rgbNew);
                                            colour.ToggleChanging();
                                        }
                                    }
                                    GUI.color = Color.white;
                                    break;
                                case Setting.SettingType.Rect: //Do nothing.
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

        public Color colour
        {
            get => Colour;
            set => Colour = value;
        }

        public Color ButtonColour
        {
            set => buttonColour = value;
        }

        public Rect WindowRect
        {
            get => windowRect;
            set => windowRect = value;
        }
        
        public ModuleManager.Category category
        {
            get => Category;
        }
    }
}