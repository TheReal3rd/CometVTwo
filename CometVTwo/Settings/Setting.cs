namespace CometVTwo.Settings
{
    public class Setting
    {
        private string name;
        private string description;
        private SettingType type;
        private bool visible = true;//TODO make the visibility update when the module is enabled & disabled.

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
        
        public bool Visible
        {
            get => visible;
            set => visible = value;
        }

        public enum SettingType//Used to identify the setting type for rendering and conversion.
        {
            Numeric,
            Logic,
            Bind,
            Enum,
            Rect,
            Colour,
            NumericSlider,
            String
        }
    }
}