using System;
using CometVTwo.Utils;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Player
{
    public class UnlimitedAmmo : Module
    {
        //Vars
        private SelectionScript selectionScript;
        public UnlimitedAmmo()
        {
            base.SetUp("UnlimitedAmmo", ModuleManager.Category.Player);
        }

        public override void OnUpdate()
        {
            selectionScript = (SelectionScript) GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
            for (int i = 0; i != selectionScript.ammoinventory.Length; i++)
            {
                selectionScript.ammoinventory[i] = Single.MaxValue;
            }
        }

        public override void OnDisable()
        {
            for (int i = 0; i != selectionScript.ammoinventory.Length; i++)
            {
               
                selectionScript.ammoinventory[i] = selectionScript.maxammo[i];
            }
        }
    }
}