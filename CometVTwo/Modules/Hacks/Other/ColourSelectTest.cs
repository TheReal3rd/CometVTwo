using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.Other
{
    public class ColourSelectTest : Module
    {
        public readonly colorSetting ColorSetting = new colorSetting("Test", Color.blue);
        
        public ColourSelectTest()
        {
            base.SetUp("ColourTest", ModuleManager.Category.Other);
            this.moduleSettings.Add(ColorSetting);
        }
    }
}