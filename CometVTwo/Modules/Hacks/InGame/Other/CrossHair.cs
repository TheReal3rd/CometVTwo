using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Other
{
    public class CrossHair : Module
    {
        private CrosshairSizeScript crosshairSizeScript;
        private CrosshairColorScript crosshairColorScript;
        
        public CrossHair()
        {
            base.SetUp("CrossHair", ModuleManager.Category.Other);
        }

        public override void OnUpdate()
        {
            crosshairSizeScript = (CrosshairSizeScript) GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript));
            crosshairColorScript = (CrosshairColorScript) GameObject.Find("Crosshair").GetComponent(typeof(CrosshairColorScript));
            crosshairColorScript.mycolor = Main.cycleColour;
        }
    }
}