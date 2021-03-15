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
            if (!multiplayerPlayerInformation.Equals(null))
            {
                DoSpoofing(multiplayerPlayerInformation.myplayer.name);
            }
        }

        private void DoSpoofing(string name)
        {
            NetworkPlayer[] networkPlayers = (NetworkPlayer[]) UnityEngine.Object.FindObjectsOfType(typeof(NetworkPlayer));
            if (networkPlayers.Length > 0)
            {
                foreach (var player in networkPlayers)
                {
                    if (player.igName == name)
                    {
                        player.NetworksteamId = UInt64.MaxValue;
                        player.steamName = "player";
                    }
                }
            }
        }
    }
}