using CometVTwo.Settings;
using CometVTwo.Utils;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Other
{
    public class Aimbot : Module
    {
        private MultiplayerPlayerScript target;
        private MyControllerScript myPlayer;
        private MyMouseLook myMouseLook;
        
        private readonly enumSetting mode = new enumSetting("Mode", "SIMPLE", new[] {"SIMPLE", "COMPLEX"});
        private readonly enumSetting mouse = new enumSetting("MouseButton", "LEFT", new[] {"LEFT", "RIGHT"});
        private readonly booleanSetting canSee = new booleanSetting("SeeTarget", true);

        public Aimbot()
        {
            //Not done.
            SetUp("Aimbot", ModuleManager.Category.Other);
            moduleSettings.Add(mode);
            moduleSettings.Add(mouse);
            moduleSettings.Add(canSee);
        }

        public override void OnUpdate()
        {
            myMouseLook = (MyMouseLook) GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
            target = GetTarget();
            if (target)
            {
                if ((Input.GetKey(KeyCode.Mouse0) && mouse.Selected == "LEFT") ||
                    (Input.GetKey(KeyCode.Mouse1) && mouse.Selected == "RIGHT"))
                {
                    myMouseLook.transform.LookAt(target.transform.position);
                }
            }
        }

        private MultiplayerPlayerScript GetTarget()
        {
            MultiplayerPlayerScript[] players = ServerUtils.MultiplayerPlayerScripts;
            myPlayer  = (MyControllerScript) GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
            foreach (MultiplayerPlayerScript player in players)
            {
                if (canSee.Value)
                {
                    RaycastHit raycastHit;
                    if (Physics.Raycast(player.transform.position, myPlayer.transform.position, out raycastHit))
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