using System;
using CometVTwo.Utils;

namespace CometVTwo.Modules.Hacks.Player
{
    public class UnlimitedAmmo : Module
    {
        public UnlimitedAmmo()
        {
            base.SetUp("UnlimitedAmmo", ModuleManager.Category.Player);
        }

        public override void OnUpdate()
        {
            SelectionScript selection = PlayerUtil.selectionScript;
            for (int i = 0; i != selection.ammoinventory.Length; i++)
            {
                selection.ammoinventory[i] = Single.MaxValue;
            }
        }

        public override void OnDisable()
        {
            SelectionScript selection = PlayerUtil.selectionScript;
            for (int i = 0; i != selection.ammoinventory.Length; i++)
            {
               
                selection.ammoinventory[i] = selection.maxammo[i];
            }
        }
    }
}