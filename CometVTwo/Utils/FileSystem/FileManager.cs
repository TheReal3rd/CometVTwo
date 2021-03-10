using CometVTwo.Modules;

namespace CometVTwo.Utils.FileSystem
{
    public class FileManager
    {
        public void SaveModule(Module module)
        {
            
        }

        public void SaveAll()
        {
            foreach (var module in Main.ModuleManager.modulesList)
            {
                SaveModule(module);
            }
        }

        public void LoadModule()
        {
            
        }
    }
}