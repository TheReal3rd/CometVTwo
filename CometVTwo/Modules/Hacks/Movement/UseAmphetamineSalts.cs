using CometVTwo.Settings;
using CometVTwo.Utils;

namespace CometVTwo.Modules.Hacks.Movement
{
    public class UseAmphetamineSalts : Module 
    {
        private readonly enumSetting mode = new enumSetting("Mode", "SUPERHOT", new[] {"SUPERHOT", "TIMESCALE"});
        private readonly doubleSetting timerSpeed = new doubleSetting("TimerSpeed", "Changes the games timescale.", -1,
            5, 0.1, 2);
        
        public UseAmphetamineSalts()
        {
            base.SetUp("UseAmphetamineSalts", ModuleManager.Category.Movement);
            this.moduleSettings.Add(timerSpeed);
            this.moduleSettings.Add(mode);
        }

        public override void OnUpdate()
        {
            if (mode.GetSelected() == "SUPERHOT")
            {
               PlayerUtil.myControllerScript.superhot = true;;
            }
            else
            {
                //TODO figure out how to get timescale to change + effect the game speed.
            }
        }

        public override void OnDisable()
        {
            PlayerUtil.myControllerScript.superhot = false;
        }
    }
}