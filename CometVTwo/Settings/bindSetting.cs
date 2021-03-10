using UnityEngine;

namespace CometVTwo.Settings
{
    public class bindSetting : Setting
    {
        private KeyCode bind;
        public bindSetting(string name, string description, KeyCode bind, SettingType type)
        {
            base.SetName(name);
            base.SetDescription(description);
            base.SetType(type);
            this.bind = bind;
        }
        public bindSetting(string name, KeyCode bind, SettingType type)
        {
            base.SetName(name);
            base.SetDescription("");
            base.SetType(type);
            this.bind = bind;
        }
        
        public KeyCode GetVelue()
        {
            return bind;
        }
        public void SetValue(KeyCode bind)
        {
            this.bind = bind;
        }
    }
}