using UnityEngine;

namespace CometVTwo.Settings
{
    public class colorSetting : Setting
    {
        private Color value;
        public bool changing = false;
        
        public colorSetting(string name, Color colour)
        {
            base.SetName(name);
            base.SetDescription("");
            base.SetType(SettingType.Colour);
            this.value = colour;
        }
        public colorSetting(string name, string description, Color colour)
        {
            base.SetName(name);
            base.SetDescription(description);
            base.SetType(SettingType.Colour);
            this.value = colour;
        }

        public Color GetValue()
        {
            return value;
        }
        public void SetValue(Color colour)
        {
            this.value = colour;
        }

        public void ToggleChanging()
        {
            changing = !changing;
        }
        public bool IsChanging()
        {
            return changing;
        }
    }
}