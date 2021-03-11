namespace CometVTwo.Modules.Hacks.Other
{
    public class SaveTest : Module
    {
        public SaveTest()
        {
            base.SetUp("SaveTest", ModuleManager.Category.Hidden);
        }

        public override void OnEnable()
        {
            Main.FileManager.SaveAll();
            Main.ModuleManager.Toggle(this);
        }
    }
}