using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.Player
{
    public class TestModule : Module
    {
        private readonly doubleSetting testValue = new doubleSetting("Test",0,100, 0.5,10, Setting.SettingType.Numeric);
        private readonly booleanSetting testValue2 = new booleanSetting("Test2", false, Setting.SettingType.Logic);
        private readonly enumSetting testValue3 =
            new enumSetting("Test3", "BOB", new string[] { "BOB", "TOM", "JOSH" }, Setting.SettingType.Enum);
        private readonly bindSetting testValue4 = new bindSetting("Test4", KeyCode.PageUp , Setting.SettingType.Bind);
        
        public TestModule()
        {
            base.SetUp("TestModule", ModuleManager.Category.Player, KeyCode.PageUp);
            base.moduleSettings.Add(testValue);
            base.moduleSettings.Add(testValue2);
            base.moduleSettings.Add(testValue3);
            base.moduleSettings.Add(testValue4);
        }
        
        public override void OnGUI()
        {
            GUI.color = Color.magenta;
            GUI.Label(new Rect(10f, 700f, 4000f, 4000f), "Test Module onGUI | testValue:"+testValue.GetValue()+" | testValue2:"+ testValue2.GetValue());
        }

    }
}