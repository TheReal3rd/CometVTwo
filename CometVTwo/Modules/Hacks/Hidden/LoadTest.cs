using UnityEngine;

namespace CometVTwo.Modules.Hacks.Other
{
    public class LoadTest : Module
    {
        public LoadTest()
        {
            base.SetUp("LoadTest", ModuleManager.Category.Hidden);
        }

        public override void OnEnable()
        {
            Main.FileManager.LoadAll();
            Main.ModuleManager.Toggle(this);
        }
    }
}