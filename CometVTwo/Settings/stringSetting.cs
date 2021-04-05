namespace CometVTwo.Settings
{
    public class stringSetting : Setting
    {
        private string value;

        public stringSetting(string name, string description, string value)
        {
            this.SetName(name);
            this.SetDescription(description);
            this.SetType(SettingType.String);
            this.Value = value;
        }
        public stringSetting(string name, string value)
        {
            this.SetName(name);
            this.SetDescription("");
            this.SetType(SettingType.String);
            this.Value = value;
        }
        public stringSetting(string name, string description, string value, bool visible)
        {
            this.SetName(name);
            this.SetDescription(description);
            this.SetType(SettingType.String);
            this.Visible = visible;
            this.Value = value;
        }
        public stringSetting(string name, string value, bool visible)
        {
            this.SetName(name);
            this.SetDescription("");
            this.SetType(SettingType.String);
            this.Visible = visible;
            this.Value = value;
        }

        public string Value
        {
            get => value;
            set => this.value = value;
        }
    }
}