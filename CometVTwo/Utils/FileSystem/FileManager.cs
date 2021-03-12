using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using CometVTwo.menu;
using CometVTwo.Modules;
using CometVTwo.Modules.Hacks.Other;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Utils.FileSystem
{
    public class FileManager
    {
        private bool logging = false;
        
        public void SaveModule(Module module)
        {
            XmlWriter writer = XmlWriter.Create(Application.dataPath+"/"+module.name+".xml");//TODO make it save in a folder plus working on Linux(Works) and Windows(Not tested yet.).
            writer.WriteStartDocument();
            writer.WriteStartElement("Settings");
            foreach (Setting setting in module.moduleSettings)
            {
                switch (setting.GetSType())
                {
                    case Setting.SettingType.Enum:
                        var selected = (enumSetting) setting;
                        writer.WriteAttributeString(selected.GetName(), selected.GetSelected());
                        break;
                    case Setting.SettingType.Bind:
                        var bind = (bindSetting) setting;
                        writer.WriteAttributeString(bind.GetName(), bind.GetVelue().ToString());
                        break;
                    case Setting.SettingType.Logic:
                        var logic = (booleanSetting) setting;
                        writer.WriteAttributeString(logic.GetName(), logic.GetValue().ToString());
                        break;
                    case Setting.SettingType.Numeric:
                        var numeric = (doubleSetting) setting;
                        writer.WriteAttributeString(numeric.GetName(), numeric.GetValue().ToString());
                        break;
                    case Setting.SettingType.Rect:
                        var rect = (rectSetting) setting;
                        Rect value = rect.GetValue();
                        string formatting = String.Format("{0},{1},{2},{3}", value.x, value.y, value.width, value.height);
                        writer.WriteAttributeString(rect.GetName(), formatting);
                        break;
                    case Setting.SettingType.Colour:
                        var colour = (colorSetting) setting;
                        int[] rgb = Utils.GetRGB(colour.GetValue());
                        string formatting1 = String.Format("{0},{1},{2}", rgb[0], rgb[1], rgb[2]);
                        writer.WriteAttributeString(colour.GetName(), formatting1);
                        break;
                }
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public void SaveAll()
        {
            foreach (var module in Main.ModuleManager.modulesList)
            {
                SaveModule(module);
            }
        }

        public void LoadModule(Module module)
        {
            XmlReader xmlReader = XmlReader.Create(Application.dataPath+"/"+module.getName()+".xml");
            while (xmlReader.Read())
            {
                foreach (Setting setting in module.moduleSettings)
                {
                    if (xmlReader.NodeType.Equals(XmlNodeType.Element) && xmlReader.Name == "Settings" && xmlReader.HasAttributes)
                    {
                        switch (setting.GetSType())
                        {
                            case Setting.SettingType.Enum:
                                var selected = (enumSetting) setting;
                                string content = xmlReader.GetAttribute(selected.GetName());
                                if (selected.GetSelection().Contains(content))
                                {
                                    selected.SetSelected(content);
                                }
                                break;
                            case Setting.SettingType.Bind:
                                var bind = (bindSetting) setting;
                                bind.SetValue(stringToKeyCode(xmlReader.GetAttribute(bind.GetName())));
                                break;
                            case Setting.SettingType.Logic:
                                var logic = (booleanSetting) setting;
                                logic.SetValue(Convert.ToBoolean(xmlReader.GetAttribute(logic.GetName())));
                                break;
                            case Setting.SettingType.Numeric:
                                var numeric = (doubleSetting) setting;
                                numeric.SetValue(Convert.ToDouble(xmlReader.GetAttribute(numeric.GetName())));
                                break;
                            case Setting.SettingType.Rect:
                                var rect = (rectSetting) setting;
                                string[] data = xmlReader.GetAttribute(rect.GetName()).Split(',');
                                Rect data1 = new Rect(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
                                foreach (WindowElement window in ClickMenu.windowList)
                                {
                                    if (window.GetCategory().Equals(rect.GetCategory()))
                                    {
                                        window.SetWindowRect(data1);
                                    }
                                }
                                break;
                            case Setting.SettingType.Colour:
                                var colour = (colorSetting) setting;
                                string[] data3 = xmlReader.GetAttribute(colour.GetName()).Split(',');
                                Color color = Utils.RGBToColour(new []{ int.Parse(data3[0]), int.Parse(data3[1]), int.Parse(data3[2]) });
                                colour.SetValue(color);
                                break;
                        }
                    }
                }
            }
            xmlReader.Close();
        }
        
        public void LoadAll()
        {
            foreach (var module in Main.ModuleManager.modulesList)
            {
                LoadModule(module);
            }
        }

        public KeyCode stringToKeyCode(string code)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (code == keyCode.ToString())
                {
                    return keyCode;
                }
            }
            return KeyCode.None;
        }

        public void Log(string data)
        {
            if (logging)
            {
                try
                {
                    StreamWriter writer = File.AppendText(Application.dataPath + "/log.txt");
                    writer.WriteLine(data);
                    writer.Close();
                }
                finally
                {
                    //Do nothing its not like we can log it lol.
                }
            }
        }
        public void SetLog(bool value)
        {
            logging = value;
        }
    }
}