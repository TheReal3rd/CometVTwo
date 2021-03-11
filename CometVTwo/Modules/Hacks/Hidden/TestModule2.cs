using CometVTwo.Utils;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.Other
{
    public class TestModule2 : Module
    {
        public TestModule2()
        {
            base.SetUp("TestModule2", ModuleManager.Category.Hidden);
        }

        public override void OnUpdate() 
        {
            
        }
        
        public override void  OnGUI()
        {
            GUI.color = Color.magenta;
            GUI.Label(new Rect(10f, 50f, 4000f, 4000f), "Test Module2 onGUI");
        }
    }
}