using CometVTwo.Settings;

namespace CometVTwo.Modules.Hacks.InGame.Movement
{
    public class Speed : Module
    {
        //Vars
        private MyControllerScript[] myControllerScript;
        //Settings
        private readonly doubleSetting walkAccel = new doubleSetting("WalkAcceleration", 0.1, 2.0, 0.1, 0.1);
        
        public Speed()
        {
            base.SetUp("Speed", ModuleManager.Category.Movement);
            this.moduleSettings.Add(walkAccel);
        }
        
        public override void OnUpdate()
        {
            myControllerScript = (MyControllerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript));
            foreach (var controllerScript in myControllerScript)
            {
                controllerScript.WalkAccel = walkAccel.GetValueFloat();
            }
        }

        public override void OnDisable()
        {
            foreach (var controllerScript in myControllerScript)
            {
                controllerScript.WalkAccel = 0.04f;
                //controllerScript.RunAccel = 0.02f;
            }
        }
    }
}