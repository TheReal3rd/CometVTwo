using CometVTwo.Settings;
using CometVTwo.Utils;

namespace CometVTwo.Modules.Hacks.Player
{
    public class RapidFire : Module
    {
        private readonly booleanSetting autoUnlimited =
            new booleanSetting("AutoUnlimitedAmmo", true);
        
        public RapidFire()
        {
            base.SetUp("RapidFire", ModuleManager.Category.Player);
            this.moduleSettings.Add(autoUnlimited);
        }

        public override void OnUpdate()
        {
            if (autoUnlimited.GetValue())
            {
                Module ammo = Main.ModuleManager.GetModule("UnlimitedAmmo");
                if (!Main.ModuleManager.IsModuleActive(ammo))
                {
                    Main.ModuleManager.Toggle(ammo);
                }
            }
            PlayerUtil.attackScript.firespeed *= 100f;
            PlayerUtil.attackScript.firespeedtimer = 10000f;
        }

        public override void OnDisable()
        {
            if (autoUnlimited.GetValue())
            {
                Module ammo = Main.ModuleManager.GetModule("UnlimitedAmmo");
                if (Main.ModuleManager.IsModuleActive(ammo))
                {
                    Main.ModuleManager.Toggle(ammo);
                }
            }
            PlayerUtil.attackScript.firespeed = 0f;
            PlayerUtil.attackScript.firespeedtimer = 0f;
        }
    }
}