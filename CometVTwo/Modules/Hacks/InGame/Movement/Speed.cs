using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Movement
{
    public class Speed : Module
    {
        //Vars
        private MyControllerScript[] myControllerScript;
        //Settings
        private readonly enumSetting mode = new enumSetting("Mode", "ACCEL", new[] {"ACCEL", "STRAFE"});
        private readonly doubleSetting walkAccel = new doubleSetting("WalkAcceleration", 0.1, 2.0, 0.1, 0.1);
        private readonly doubleSetting bunnySpeed = new doubleSetting("BunnySpeed", 0.5, 2.0, 0.1, 0.5);
        
        public Speed()
        {
            base.SetUp("Speed", ModuleManager.Category.Movement);
            this.moduleSettings.Add(mode);
            this.moduleSettings.Add(walkAccel);
            this.moduleSettings.Add(bunnySpeed);
        }
        
        public override void OnUpdate()
        {
            myControllerScript = (MyControllerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript));
            switch (mode.Selected)//This the point i discovered strings can go through a switch. It changed my life.
            {
                case "ACCEL":
                    foreach (var controllerScript in myControllerScript)
                    {
                        controllerScript.WalkAccel = walkAccel.GetValueFloat();
                    }
                    break;
                case "STRAFE"://Pasted from Future big client epik client 0x22 big dev 100
                    foreach (var controllerScript in myControllerScript)
                    {
                        if (Input.GetKey(KeyCode.Space) && controllerScript.CheckGrounded())
                        {
                            controllerScript.dojump = true;
                            controllerScript.bunnyspeed += bunnySpeed.GetValueFloat();
                            controllerScript.maxbunnyspeed = bunnySpeed.GetValueFloat();
                        }
                        else if(!controllerScript.CheckGrounded())
                        {
                            controllerScript.bunnyspeed += 0.0005f;
                        }
                        else
                        {
                            controllerScript.bunnyspeed = 0.0f;
                        }
                    }
                    break;
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