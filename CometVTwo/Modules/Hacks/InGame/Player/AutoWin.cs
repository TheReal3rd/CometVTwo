using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Player
{
    public class AutoWin : Module
    {
        //Vars
        private MultiplayerPlayerInformation multiplayerPlayerInformation;
        private MultiplayerPlayerScript[] MultiplayerPlayerScripts;
        //Settings
        private readonly booleanSetting noneHost = new booleanSetting("NoneHost", false);
        
        public AutoWin()
        {
            base.SetUp("AutoWin",ModuleManager.Category.Player);
            this.moduleSettings.Add(noneHost);
        }

        public override void OnEnable()
        {
            multiplayerPlayerInformation = (MultiplayerPlayerInformation) GameObject.Find("Player").GetComponent(typeof(MultiplayerPlayerInformation));
            MultiplayerPlayerScripts = (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript));
            foreach (var clients in MultiplayerPlayerScripts)
            {
                if (clients.name == multiplayerPlayerInformation.myplayer.name)
                {
                    if (noneHost.Value)
                    {
                        clients.CmdIncreaseKills(116123);
                    }
                    clients.CmdWinRound(multiplayerPlayerInformation.myplayer.name);
                    clients.CmdSendChatMessage("ez!");
                    clients.CmdSendChatMessage("So ez!!!1!");
                }
            }
            Main.ModuleManager.Toggle(this);
        }
    }
}