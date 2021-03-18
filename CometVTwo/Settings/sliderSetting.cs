namespace CometVTwo.Settings
{
    public class sliderSetting : Setting
    {
        //We're using doubles cause they can be easily converted to Integers and Floats.
        private double min;
        private double max;
        private double value;

        public sliderSetting(string name, string desciption, double min, double max, double defaultValue)
        {
            this.SetName(name);
            this.SetDescription(desciption);
            this.SetType(SettingType.NumericSlider);
            this.min = min;
            this.max = max;
            this.value = defaultValue;
        }
        public sliderSetting(string name, string desciption, double min, double max, double defaultValue, bool visible)
        {
            this.SetName(name);
            this.SetDescription(desciption);
            this.SetType(SettingType.NumericSlider);
            this.min = min;
            this.max = max;
            this.value = defaultValue;
            this.Visible = visible;
        }
        public sliderSetting(string name, double min, double max, double defaultValue)
        {
            this.SetName(name);
            this.SetDescription("");
            this.SetType(SettingType.NumericSlider);
            this.min = min;
            this.max = max;
            this.value = defaultValue;
        }
        public sliderSetting(string name, double min, double max, double defaultValue, bool visible)
        {
            this.SetName(name);
            this.SetDescription("");
            this.SetType(SettingType.NumericSlider);
            this.min = min;
            this.max = max;
            this.value = defaultValue;
            this.Visible = visible;
        }
        public double GetValue()
        {
            return value;
        }
        public int GetValueInt()
        {
            return (int) value;
        }
        public float GetValueFloat()
        {
            return (float) value;
        }

        public void SetValue(double value)
        {
            this.value = value;
        }

        public double MAX
        {
            get => max;
        }

        public double MIN
        {
            get => min;
        }
    }
}