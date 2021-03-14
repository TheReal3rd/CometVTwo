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
        public booleanSetting(string name, string description, bool value, bool visible)
        {
            this.SetName(name);
            this.SetDescription(description);
            this.SetType(SettingType.Logic);
            this.value = value;
            this.Visible = visible;
        }
        public booleanSetting(string name, bool value)
        {
            this.SetName(name);
            this.SetDescription("");
            this.SetType(SettingType.Logic);
            this.value = value;
        }
        public booleanSetting(string name, bool value, bool visible)
        {
            this.SetName(name);
            this.SetDescription("");
            this.SetType(SettingType.Logic);
            this.value = value;
            this.Visible = visible;
        }

        public bool Value
        {
            get => value;
            set => this.value = value;
        }

        public void Toggle()
        {
            value = !value;
        }
    }
}