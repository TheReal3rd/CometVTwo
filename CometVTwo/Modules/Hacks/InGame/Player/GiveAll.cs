using CometVTwo.Utils;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Player
{
    public class GiveAll : Module
    {
        private SelectionScript selectionScript;
        public GiveAll()
        {
            base.SetUp("GiveAll", ModuleManager.Category.Player);
        }

        public override void OnEnable()
        {
            selectionScript = (SelectionScript) GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
            GiveKeys();
            GiveAllWeapons();
            GiveMaxAmmo();
            Main.ModuleManager.Toggle(this);
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