using CometVTwo.Modules;
using UnityEngine;

namespace CometVTwo.Settings
{
    public class rectSetting : Setting
    {
        //This setting is for GUI.
        private Rect value;
        private bool update = false;
        public rectSetting(string name, Rect rect)
        {
            base.SetName(name);
            base.SetDescription("");
            base.SetType(SettingType.Rect);
        }
        public rectSetting(string name, string description, Rect rect)
        {
            base.SetName(name);
            base.SetDescription(description);
            base.SetType(SettingType.Rect);
        }
        
        public Rect GetValue()
        {
            return value;
        }
        public void SetValue(Rect rect)
        {
            this.value = rect;
        }

        public bool Update
        {
            get => update;
            set => update = value;
        }
    }
}