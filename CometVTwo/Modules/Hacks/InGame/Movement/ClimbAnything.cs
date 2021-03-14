using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Movement
{
    public class ClimbAnything : Module
    {
        private LadderUseScript ladderUseScript;
        
        public ClimbAnything()
        {
            base.SetUp("CimbAnything", ModuleManager.Category.Movement);
        }

        public override void OnUpdate()
        {
            ladderUseScript = (LadderUseScript) GameObject.Find("Player").GetComponent(typeof(LadderUseScript));
            ladderUseScript.climbanything = true;
        }

        public override void OnDisable()
        {
            ladderUseScript.climbanything = false;
        }
    }
}