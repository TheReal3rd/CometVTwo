using CometVTwo.Settings;

namespace CometVTwo.Modules.Hacks.InGame.Movement
{
    public class Speed : Module
    {
        //Vars
        private MyControllerScript[] myControllerScript;
        //Settings
        private readonly doubleSetting maxTotalSpeed = new doubleSetting("MaxSpeed", 1, 20, 1, 2);
        private readonly doubleSetting maxRunSpeed = new doubleSetting("MaxRunSpeed", 1, 20, 1, 2);
        private readonly doubleSetting maxBunnySpeed = new doubleSetting("MaxBunnySpeed", 1, 20, 1, 2);
        private readonly doubleSetting walkSpeed = new doubleSetting("WalkSpeed", 1, 20, 1, 2);
        private readonly doubleSetting bunnySpeed = new doubleSetting("BunnySpeed", 1, 20, 1, 2);
        private readonly doubleSetting walkAccel = new doubleSetting("WalkAcceleration", 0.1, 2.0, 0.1, 0.1);
        public Speed()
        {
            base.SetUp("Speed", ModuleManager.Category.Movement);
            this.moduleSettings.Add(maxTotalSpeed);
            this.moduleSettings.Add(maxRunSpeed);
            this.moduleSettings.Add(maxBunnySpeed);
            this.moduleSettings.Add(walkSpeed);
            this.moduleSettings.Add(bunnySpeed);
            this.moduleSettings.Add(walkAccel);
        }
        
        public override void OnUpdate()
        {
            myControllerScript = (MyControllerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript));
            foreach (var controllerScript in myControllerScript)
            {
                controllerScript.MaxTotalSpeed = maxTotalSpeed.GetValueFloat();
                controllerScript.MaxRunSpeed = maxRunSpeed.GetValueFloat();
                controllerScript.maxbunnyspeed = maxBunnySpeed.GetValueFloat();
                controllerScript.WalkSpeed = walkSpeed.GetValueFloat();
                controllerScript.bunnyspeed = bunnySpeed.GetValueFloat();
                controllerScript.WalkAccel = walkAccel.GetValueFloat();
            }
        }
    }
}