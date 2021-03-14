using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Other
{
    public class CrossHair : Module
    {
        private CrosshairColorScript crosshairColorScript;
        
        public CrossHair()
        {
            base.SetUp("CrossHair", ModuleManager.Category.Other);
        }

        public override void OnUpdate()
        {
            crosshairColorScript = (CrosshairColorScript) GameObject.Find("Crosshair").GetComponent(typeof(CrosshairColorScript));
            crosshairColorScript.mycolor = Main.cycleColour;
        }
    }
}