using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Movement
{
    public class UseAmphetamineSalts : Module //Figure out a way of changing timescale.
    {
        //Vars
        private MyControllerScript myControllerScript;
        
        //Settings
        //private readonly enumSetting mode = new enumSetting("Mode", "SUPERHOT", new[] {"SUPERHOT", "TIMESCALE"});
        private readonly doubleSetting timerSpeed = new doubleSetting("TimerSpeed", "Changes the games timescale.", -1,
            5, 0.1, 2);
        
        public UseAmphetamineSalts()
        {
            base.SetUp("UseAmphetamineSalts", ModuleManager.Category.Movement);//My guy Amp 
            this.moduleSettings.Add(timerSpeed);
            //this.moduleSettings.Add(mode);
        }

        public override void OnUpdate()
        {
            myControllerScript = (MyControllerScript) GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
            myControllerScript.superhot = true;;
        }

        public override void OnDisable()
        {
            myControllerScript.superhot = false;
        }
    }
}