using CometVTwo.Modules;
using UnityEngine;

namespace CometVTwo.Settings
{
    public class rectSetting : Setting
    {
        //This setting is for GUI.
        private Rect value;
        private ModuleManager.Category Category;
        public rectSetting(string name, Rect rect, ModuleManager.Category category)
        {
            base.SetName(name);
            base.SetDescription("");
            base.SetType(SettingType.Rect);
            this.Category = category;
        }
        public rectSetting(string name, string description, Rect rect, ModuleManager.Category category)
        {
            base.SetName(name);
            base.SetDescription(description);
            base.SetType(SettingType.Rect);
            this.Category = category;
        }

        public ModuleManager.Category GetCategory()
        {
            return this.Category;
        }
        public Rect GetValue()
        {
            return value;
        }
        public void SetValue(Rect rect)
        {
            this.value = rect;
        }
    }
}