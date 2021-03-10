using System;

namespace CometVTwo.Settings
{
    public class doubleSetting : Setting
    {
        //We're using doubles cause they can be easily converted to Integers and Floats.
        private double min;
        private double max;
        private double value;
        private double incrementAmount;

        public doubleSetting(string name, string desciption, double min, double max, double incrementAmount, double defaultValue, SettingType type)
        {
            this.SetName(name);
            this.SetDescription(desciption);
            this.SetType(type);
            this.min = min;
            this.max = max;
            this.incrementAmount = incrementAmount;
            this.value = defaultValue;
        }
        public doubleSetting(string name, double min, double max, double incrementAmount, double defaultValue, SettingType type)
        {
            this.SetName(name);
            this.SetDescription("");
            this.SetType(type);
            this.min = min;
            this.max = max;
            this.incrementAmount = incrementAmount;
            this.value = defaultValue;
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

        public void Increase()
        {
            value += incrementAmount;
            if (value > max)
            {
                value = max;
            }
            else if (value < min)
            {
                value = min;
            }
        }

        public void Decrease()
        {
            value -= incrementAmount;
            if (value > max)
            {
                value = max;
            }
            else if (value < min)
            {
                value = min;
            }
        }

    }
}