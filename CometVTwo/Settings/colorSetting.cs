using UnityEngine;

namespace CometVTwo.Settings
{
    public class colorSetting : Setting//Colour* fuck!
    {
        private Color value;
        private bool changing;
        public int[] rgbNew;

        public colorSetting(string name, string description, Color colour)
        {
            base.SetName(name);
            base.SetDescription(description);
            base.SetType(SettingType.Colour);
            this.value = colour;
        }
        public colorSetting(string name, string description, Color colour, bool visible)
        {
            base.SetName(name);
            base.SetDescription(description);
            base.SetType(SettingType.Colour);
            this.value = colour;
            this.Visible = visible;
        }
        public colorSetting(string name, Color colour)
        {
            base.SetName(name);
            base.SetDescription("");
            base.SetType(SettingType.Colour);
            this.value = colour;
        }
        public colorSetting(string name, Color colour, bool visible)
        {
            base.SetName(name);
            base.SetDescription("");
            base.SetType(SettingType.Colour);
            this.value = colour;
            this.Visible = visible;
        }

        public Color Value
        {
            get => value;
            set => this.value = value;
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