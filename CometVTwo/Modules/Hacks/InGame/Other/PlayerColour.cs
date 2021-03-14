using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Other
{
    public class PlayerColour : Module
    {
        //Vars
        private MultiplayerPlayerInformation multiplayerPlayerInformation;
        private MultiplayerPlayerScript[] multiplayerPlayerScript;
        
        //Settings
        private readonly booleanSetting rainbowEveryone = new booleanSetting("RainbowEveryone", false);
        
        public PlayerColour()
        {
            base.SetUp("PlayerColour", ModuleManager.Category.Other);
            this.moduleSettings.Add(rainbowEveryone);
        }

        public override void OnUpdate()
        {
            multiplayerPlayerScript = (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript));
            multiplayerPlayerInformation = (MultiplayerPlayerInformation) GameObject.Find("Player").GetComponent(typeof(MultiplayerPlayerInformation));
            Color colour = Main.cycleColour;
            foreach (var clients in multiplayerPlayerScript)
            {
                if (rainbowEveryone.Value)
                {
                    clients.modelmat.SetColor("_PrimaryMaskColor", colour);
                    clients.modelmat.SetColor("_SecondaryMaskColor", colour);
                    clients.modelmat.SetColor("_DetailMaskColor", colour);
                }
                else
                {
                    if (clients.name == multiplayerPlayerInformation.myplayer.name)
                    {
                        clients.modelmat.SetColor("_PrimaryMaskColor", colour);
                        clients.modelmat.SetColor("_SecondaryMaskColor", colour);
                        clients.modelmat.SetColor("_DetailMaskColor", colour);
                    }
                }
            }
        }
    }
}