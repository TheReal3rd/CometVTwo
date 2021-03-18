using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Movement
{
    public class UseAmphetamineSalts : Module //Figure out a way of changing timescale.
    {
        //Vars
        private MyControllerScript myControllerScript;
        //Settings

        public UseAmphetamineSalts()
        {
            base.SetUp("UseAmphetamineSalts", ModuleManager.Category.Movement);//My guy Amp 
        }

        public override void OnUpdate()
        {
            myControllerScript = (MyControllerScript) GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
            myControllerScript.superhot = true;
        }

        public override void OnDisable()
        {
            if (myControllerScript.superhot)
            {
                myControllerScript.superhot = false;
            }
        }
    }
}