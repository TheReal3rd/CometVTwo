using System;

namespace CometVTwo.Settings
{
    public class booleanSetting : Setting
    {
        private bool value;
        
        public booleanSetting(string name, string description, bool value)
        {
            this.SetName(name);
            this.SetDescription(description);
            this.SetType(SettingType.Logic);
            this.value = value;
        }
        public booleanSetting(string name, bool value)
        {
            this.SetName(name);
            this.SetDescription("");
            this.SetType(SettingType.Logic);
            this.value = value;
        }

        public bool GetValue()
        {
            return value;
        }
        public void SetValue(bool value)
        {
            this.value = value;
        }

        public void Toggle()
        {
            value = !value;
        }
    }
}