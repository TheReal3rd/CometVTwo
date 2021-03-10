using CometVTwo.Utils;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.Player
{
    public class GiveAll : Module
    {
        public GiveAll()
        {
            base.SetUp("GiveAll", ModuleManager.Category.Player);
        }

        public override void OnEnable()
        {
            PlayerUtil.GiveKeys();
            PlayerUtil.GiveAllWeapons();
            PlayerUtil.GiveMaxAmmo();
            Main.ModuleManager.Toggle(this);
        }

        public override void OnGUI()
        {
            GUI.color = Color.magenta;
            GUI.Label(new Rect(10f, 700f, 4000f, 4000f), "Test Module onGUI");
        }

    }
}