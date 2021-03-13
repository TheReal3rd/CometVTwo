using CometVTwo.Settings;
using CometVTwo.Utils;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.Other
{
    public class Aimbot : Module
    {
        private MultiplayerPlayerScript target = null;
        
        private readonly enumSetting mode = new enumSetting("Mode", "SIMPLE", new[] {"SIMPLE", "COMPLEX"});
        private readonly enumSetting mouse = new enumSetting("MouseButton", "LEFT", new[] {"LEFT", "RIGH"});
        private readonly booleanSetting canSee = new booleanSetting("SeeTarget", true);

        public Aimbot()
        {
            SetUp("Aimbot", ModuleManager.Category.Other);
            moduleSettings.Add(mode);
            moduleSettings.Add(mouse);
            moduleSettings.Add(canSee);
        }

        public override void OnUpdate()
        {
            target = GetTarget();
            if (target)
            {
                if ((Input.GetKey(KeyCode.Mouse0) && mouse.GetSelected() == "LEFT") ||
                    (Input.GetKey(KeyCode.Mouse1) && mouse.GetSelected() == "RIGHT"))
                {
                    MyMouseLook mouseLook = PlayerUtil.myMouseLook;
                    mouseLook.transform.LookAt(target.NetworksyncPos);
                }
            }
        }

        private MultiplayerPlayerScript GetTarget()
        {
            MultiplayerPlayerScript[] players = ServerUtils.MultiplayerPlayerScripts;
            MyControllerScript myPlayer = PlayerUtil.myControllerScript;
            foreach (MultiplayerPlayerScript player in players)
            {
                if (canSee.GetValue())
                {
                    RaycastHit raycastHit;
                    if (Physics.Raycast(player.NetworksyncPos, myPlayer.transform.position, out raycastHit))
                    {
                        if (raycastHit.transform != myPlayer.transform.parent)
                        {
                            continue;
                        }
                    }
                }

                return player;
            }

            return null;
        }
    }
}