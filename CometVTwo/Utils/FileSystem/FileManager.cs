using System;
using System.Xml;
using CometVTwo.Modules;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Utils.FileSystem
{
    public class FileManager
    {
        public void SaveModule(Module module)
        {
            XmlWriter writer = XmlWriter.Create(Application.dataPath+"/"+module.name+".xml");//TODO make it save in a folder plus working on Linux and Windows.
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
                    if (xmlReader.NodeType.Equals(XmlNodeType.Element) && xmlReader.Name == "Settings" &&
                        xmlReader.HasAttributes)
                    {
                        switch (setting.GetSType())
                        {
                            case Setting.SettingType.Enum:
                                var selected = (enumSetting) setting;
                                selected.SetSelected(xmlReader.GetAttribute(selected.GetName()));
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
    }
}