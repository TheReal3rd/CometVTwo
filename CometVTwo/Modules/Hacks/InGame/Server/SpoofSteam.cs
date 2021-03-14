using System;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Server
{
    public class SpoofSteam : Module//IDK if this will work...
    {
        private MultiplayerPlayerInformation multiplayerPlayerInformation;

        public SpoofSteam()
        {
            base.SetUp("SpoofSteam", ModuleManager.Category.Server);
        }

        public override void OnUpdate()
        {
            multiplayerPlayerInformation = (MultiplayerPlayerInformation) GameObject.Find("Player").GetComponent(typeof(MultiplayerPlayerInformation));
            DoSpoofing(multiplayerPlayerInformation.myplayer.name);
        }

        public void DoSpoofing(string name)
        {
            foreach (var player in (NetworkPlayer[]) UnityEngine.Object.FindObjectsOfType(typeof(NetworkPlayer)))
            {
                if (player.igName == name)
                {
                    player.steamId = UInt64.MaxValue;
                    player.steamName = "player";
                }
            }
        }
    }
}