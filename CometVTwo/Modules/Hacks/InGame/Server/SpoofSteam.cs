using System;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Server
{
    public class SpoofSteam : Module//IDK if this will work...
    {
        public SpoofSteam()
        {
            base.SetUp("SpoofSteam", ModuleManager.Category.Server);
        }

        public override void OnUpdate()
        {
            MultiplayerPlayerInformation multiplayerPlayerInformation = (MultiplayerPlayerInformation) GameObject.Find("Player").GetComponent(typeof(MultiplayerPlayerInformation));
            NetworkPlayer[] networkPlayers = (NetworkPlayer[]) UnityEngine.Object.FindObjectsOfType(typeof(NetworkPlayer));
            if (networkPlayers.Length > 0 && !multiplayerPlayerInformation.Equals(null))
            {
                foreach (NetworkPlayer player in networkPlayers)
                {
                    if (!player.Equals(null) && !string.IsNullOrEmpty(player.igName))
                    {
                        if (player.igName == multiplayerPlayerInformation.myplayer.name)
                        {
                            player.NetworksteamId = UInt64.MaxValue;
                            player.steamName = "player";
                        }
                    }
                }
            }
        }
    }
}