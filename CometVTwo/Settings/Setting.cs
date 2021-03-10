using System;
using System.Collections.Generic;

namespace CometVTwo.Settings
{
    public class Setting
    {
        private string name;
        private string description;
        private SettingType type;
        
        public Setting(string name, string description, SettingType type)
        {
            this.name = name;
            this.description = description;
            this.type = type;
        }
        public Setting(string name, SettingType type)
        {
            this.name = name;
            this.description = "";
            this.type = type;
        }
        public Setting()
        {
            //Keep blank fixes the Other settings.
        }

        public string GetName()
        {
            return name;
        }
        
        public string GetDescription()
        {
            return description;
        }

        public SettingType GetSType()
        {
            return type;
        }

        public void SetName(string name)
        {
            this.name = name;
        }
        
        public void SetDescription(string description)
        {
            this.description = description;
        }

        public void SetType(SettingType type)
        {
            this.type = type;
        }
        
        public enum SettingType//Used to identify the setting type for rendering and conversion.
        {
            Numeric,
            Logic,
            Bind,
            Enum
        }
    }
}