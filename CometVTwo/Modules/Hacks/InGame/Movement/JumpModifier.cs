using CometVTwo.Settings;
using CometVTwo.Utils;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Movement
{
    public class JumpModifier : Module
    {
        //Vars
        private MyControllerScript myControllerScript;
        
        //Settings
        private readonly doubleSetting height =
            new doubleSetting("Height", "The height the player can jump.", 0.1, 2, 0.1, 1);
        private readonly booleanSetting allowGravityForce =
            new booleanSetting("AllowGravityForce", false);
        private readonly doubleSetting gravity =
            new doubleSetting("GravityForce", "Changes gravities force.", -2.0, 2.0, 0.05, 1);
        
        public JumpModifier()
        {
            base.SetUp("JumpModifier", ModuleManager.Category.Movement);
            base.moduleSettings.Add(height);
            base.moduleSettings.Add(allowGravityForce);
            base.moduleSettings.Add(gravity);
        }

        public override void OnUpdate()
        {
            myControllerScript = (MyControllerScript) GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
            myControllerScript.jumpamount = height.GetValueFloat();
            if (allowGravityForce.Value)
            {
                myControllerScript.gravityforce = gravity.GetValueFloat();
            }
        }

        public override void OnDisable()
        {
            myControllerScript.jumpamount = 0.2f;
        }
    }
}