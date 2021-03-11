using UnityEngine;

namespace CometVTwo.Settings
{
    public class bindSetting : Setting
    {
        private KeyCode bind;
        public bindSetting(string name, string description, KeyCode bind)
        {
            base.SetName(name);
            base.SetDescription(description);
            base.SetType(SettingType.Bind);
            this.bind = bind;
        }
        public bindSetting(string name, KeyCode bind)
        {
            base.SetName(name);
            base.SetDescription("");
            base.SetType(SettingType.Bind);
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