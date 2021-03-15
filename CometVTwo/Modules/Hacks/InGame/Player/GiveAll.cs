using CometVTwo.Settings;

using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Player
{
    public class GiveAll : Module
    {
        //Vars
        private SelectionScript selectionScript;
        //Settings
        private readonly enumSetting mode = new enumSetting("Mode", "SINGLE", new[] {"SINGLE", "AUTO"});
        private readonly booleanSetting autoUnlimitedAmmo = new booleanSetting("AutoUnlimitedAmmo-AUTO", false);
        
        public GiveAll()
        {
            base.SetUp("GiveAll", ModuleManager.Category.Player);
            this.moduleSettings.Add(mode);
            this.moduleSettings.Add(autoUnlimitedAmmo);
        }

        public override void OnEnable()
        {
            selectionScript = (SelectionScript) GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
            GiveMaxAmmo();
        }

        public override void OnDisable()
        {
            if (autoUnlimitedAmmo.Value)
            {
                UnlimitedAmmo unlimitedAmmo = (UnlimitedAmmo) Main.ModuleManager.GetModule("UnlimitedAmmo");
                if (Main.ModuleManager.IsModuleActive(unlimitedAmmo))
                {
                    Main.ModuleManager.Toggle(unlimitedAmmo);
                }
            }
        }

        public override void OnUpdate()
        {
            selectionScript = (SelectionScript) GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
            GiveKeys();
            GiveAllWeapons();
            if (mode.Selected == "AUTO" && autoUnlimitedAmmo.Value)
            {
                UnlimitedAmmo unlimitedAmmo = (UnlimitedAmmo) Main.ModuleManager.GetModule("UnlimitedAmmo");
                if (!Main.ModuleManager.IsModuleActive(unlimitedAmmo))
                {
                    Main.ModuleManager.Toggle(unlimitedAmmo);
                }
            }
            if (mode.Selected == "SINGLE")
            {
                Main.ModuleManager.Toggle(this);
            }
        }

        private void GiveMaxAmmo()
        {
            for (int i = 0; i != selectionScript.ammoinventory.Length; i++)
            {
                var maxAmmo = selectionScript.maxammo[i];
                selectionScript.ammoinventory[i] = maxAmmo;
            }
        }
        
        private void GiveKeys()
        {
            if (selectionScript)
            {
                selectionScript.haveredkey = true;
                selectionScript.havebluekey = true;
                selectionScript.haveyellowkey = true;
            }
        }
        
        private void GiveAllWeapons()
        {
            if (selectionScript)
            {
                for (int i = 0; i < selectionScript.weaponinventory.Length; i++)
                {
                    selectionScript.weaponinventory[i] = true;
                }
                selectionScript.havedualpistols = true;
                selectionScript.permduals = true;
                selectionScript.permdaikatana = true;
                selectionScript.havedaikatana = true;
                selectionScript.permshotguns = true;
                selectionScript.havedualshotguns = true;
            }
        }
    }
}