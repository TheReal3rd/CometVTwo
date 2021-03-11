using CometVTwo.Settings;
using CometVTwo.Utils;

namespace CometVTwo.Modules.Hacks.Movement
{
    public class JumpModifier : Module
    {
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
            PlayerUtil.myControllerScript.jumpamount = height.GetValueFloat();
            if (allowGravityForce.GetValue())
            {
                PlayerUtil.myControllerScript.gravityforce = gravity.GetValueFloat();
            }
        }

        public override void OnDisable()
        {
            PlayerUtil.myControllerScript.jumpamount = 0.2f;
        }
    }
}