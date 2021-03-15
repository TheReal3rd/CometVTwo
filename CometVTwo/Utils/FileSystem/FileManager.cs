using System;
using System.IO;
using System.Linq;
using System.Xml;
using CometVTwo.Modules;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Utils.FileSystem
{
    public class FileManager
    {
        private bool logging = false;

        public void SaveModule(Module module)
        {
            string path = Application.dataPath + "/comet/" + module.name + ".xml";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(Application.dataPath + "/comet");
            }
            XmlWriter writer = XmlWriter.Create(path);
            writer.WriteStartDocument();
            writer.WriteStartElement("Settings");
            foreach (Setting setting in module.moduleSettings)
            {
                switch (setting.GetSType())
                {
                    case Setting.SettingType.Enum:
                        var selected = (enumSetting) setting;
                        writer.WriteAttributeString(selected.GetName(), selected.Selected);
                        break;
                    case Setting.SettingType.Bind:
                        var bind = (bindSetting) setting;
                        writer.WriteAttributeString(bind.GetName(), bind.Bind.ToString());
                        break;
                    case Setting.SettingType.Logic:
                        var logic = (booleanSetting) setting;
                        writer.WriteAttributeString(logic.GetName(), logic.Value.ToString());
                        break;
                    case Setting.SettingType.Numeric:
                        var numeric = (doubleSetting) setting;
                        writer.WriteAttributeString(numeric.GetName(), numeric.GetValue().ToString());
                        break;
                    case Setting.SettingType.Rect:
                        var rect = (rectSetting) setting;
                        Rect value = rect.Value;
                        if (value.width == 0)
                        {
                            value.width = 360;
                            value.height = 400;
                        }
                        string formatting = String.Format("{0},{1},{2},{3}", value.x, value.y, value.width, value.height);
                        writer.WriteAttributeString(rect.GetName(), formatting);
                        break;
                    case Setting.SettingType.Colour:
                        var colour = (colorSetting) setting;
                        int[] rgb = Utils.GetRGB(colour.Value);
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
                try
                {
                    SaveModule(module);
                }
                catch (Exception e)
                {
                    Main.FileManager.Log("Failed to save "+module.getName()+" settings! Error:\n\n"+e.ToString()+"\n\n");
                }
            }
        }

        public void LoadModule(Module module)
        {
            string path = Application.dataPath + "/comet/" + module.getName() + ".xml";
            if (File.Exists(path))
            {
                XmlReader xmlReader = XmlReader.Create(path);
                while (xmlReader.Read())
                {
                    foreach (Setting setting in module.moduleSettings)
                    {
                        if (xmlReader.NodeType.Equals(XmlNodeType.Element) && xmlReader.Name == "Settings" &&
                            xmlReader.HasAttributes)
                        {
                            switch (setting.GetSType())
                            {
                                case Setting.SettingType.Enum:
                                    var selected = (enumSetting) setting;
                                    string content = xmlReader.GetAttribute(selected.GetName());
                                    if (selected.GetSelection().Contains(content))
                                    {
                                        selected.Selected = content;
                                    }

                                    break;
                                case Setting.SettingType.Bind:
                                    var bind = (bindSetting) setting;
                                    bind.Bind = stringToKeyCode(xmlReader.GetAttribute(bind.GetName()));
                                    break;
                                case Setting.SettingType.Logic:
                                    var logic = (booleanSetting) setting;
                                    logic.Value = Convert.ToBoolean(xmlReader.GetAttribute(logic.GetName()));
                                    break;
                                case Setting.SettingType.Numeric:
                                    var numeric = (doubleSetting) setting;
                                    numeric.SetValue(Convert.ToDouble(xmlReader.GetAttribute(numeric.GetName())));
                                    break;
                                case Setting.SettingType.Rect:
                                    var rect = (rectSetting) setting;
                                    string[] data = xmlReader.GetAttribute(rect.GetName()).Split(',');
                                    Rect data1 = new Rect(float.Parse(data[0]), float.Parse(data[1]),
                                        float.Parse(data[2]), float.Parse(data[3]));
                                    if (data1.width == 0)
                                    {
                                        data1.width = 360;
                                        data1.height = 400;
                                    }

                                    rect.Update = true;
                                    rect.Value = data1;
                                    break;
                                case Setting.SettingType.Colour:
                                    var colour = (colorSetting) setting;
                                    string[] data3 = xmlReader.GetAttribute(colour.GetName()).Split(',');
                                    Color color = Utils.RGBToColour(new[]
                                        {int.Parse(data3[0]), int.Parse(data3[1]), int.Parse(data3[2])});
                                    colour.Value = color;
                                    break;
                            }
                        }
                    }
                }

                xmlReader.Close();
            }
        }
        
        public void LoadAll()
        {
            foreach (var module in Main.ModuleManager.modulesList)
            {
                try
                {
                    LoadModule(module);
                }
                catch (Exception e)
                {
                    Main.FileManager.Log("Failed to load "+module.getName()+" settings! Error:\n\n"+e+"\n\n");
                }
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
                    StreamWriter writer = File.AppendText(Application.dataPath + "/comet/log.txt");
                    writer.WriteLine(data);
                    writer.Close();
                }
                catch (Exception e)
                {
                    //Do nothing we can't log it if the logger breaks.
                }
            }
        }

        public void SetLog(bool value)
        {
            logging = value;
        }
    }
}