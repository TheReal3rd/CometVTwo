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
        public bindSetting(string name, string description, KeyCode bind, bool visible)
        {
            base.SetName(name);
            base.SetDescription(description);
            base.SetType(SettingType.Bind);
            this.bind = bind;
            this.Visible = visible;
        }
        public bindSetting(string name, KeyCode bind)
        {
            base.SetName(name);
            base.SetDescription("");
            base.SetType(SettingType.Bind);
            this.bind = bind;
        }
        public bindSetting(string name, KeyCode bind, bool visible)
        {
            base.SetName(name);
            base.SetDescription("");
            base.SetType(SettingType.Bind);
            this.Visible = visible;
            this.bind = bind;
        }

        public KeyCode Bind
        {
            get => bind;
            set => bind = value;
        }
    }
}