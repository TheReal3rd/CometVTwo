using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Player
{
    public class RapidFire : Module
    {
        //Vars
        private AttackScript attackScript;
        
        //Settings
        private readonly booleanSetting autoUnlimited =
            new booleanSetting("AutoUnlimitedAmmo", true);
        
        public RapidFire()
        {
            base.SetUp("RapidFire", ModuleManager.Category.Player);
            this.moduleSettings.Add(autoUnlimited);
        }

        public override void OnUpdate()
        {
            attackScript = (AttackScript) GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript));
            if (autoUnlimited.Value)
            {
                Module ammo = Main.ModuleManager.GetModule("UnlimitedAmmo");
                if (!Main.ModuleManager.IsModuleActive(ammo))
                {
                    Main.ModuleManager.Toggle(ammo);
                }
            }
            attackScript.firespeed *= 100f;
            attackScript.firespeedtimer = 10000f;
        }

        public override void OnDisable()
        {
            if (autoUnlimited.Value)
            {
                Module ammo = Main.ModuleManager.GetModule("UnlimitedAmmo");
                if (Main.ModuleManager.IsModuleActive(ammo))
                {
                    Main.ModuleManager.Toggle(ammo);
                }
            }
            attackScript.firespeed = 0f;
            attackScript.firespeedtimer = 0f;
        }
    }
}