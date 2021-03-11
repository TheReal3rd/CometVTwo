using CometVTwo.Settings;
using CometVTwo.Utils;

namespace CometVTwo.Modules.Hacks.Player
{
    public class GodMode : Module
    {
        private readonly enumSetting modes =
            new enumSetting("Mode", "BUILTIN", new[] {"BUILTIN", "HEALTH", "SET"});
        private readonly doubleSetting health =
            new doubleSetting("Health", 0, 1000, 10, 500);
        
        public GodMode()
        {
            base.SetUp("GodMode", ModuleManager.Category.Player);
            this.moduleSettings.Add(modes);
            this.moduleSettings.Add(health);
        }

        public override void OnUpdate()
        {
            if (modes.GetSelected() == "BUILTIN")
            {
                PlayerUtil.BuiltInGodMode();
            }
            else if(modes.GetSelected() == "HEALTH") 
            {
                PlayerUtil.HealthGodMode();
            }
            else
            {
                PlayerUtil.SetPlayerHealth(health.GetValueFloat());
            }
        }
    }
}