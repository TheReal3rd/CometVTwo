using CometVTwo.Settings;
using CometVTwo.Utils.Objects;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Player
{
    public class Username : Module
    {
        //Vars
        private TimeDelay delayTimer = new TimeDelay();
        private int frameCounter = 0;
        private readonly string[] snake = new[]
        {
            ">",
            "~>",
            "~~>",
            "~~~>",
            "~~~~>",
            "~~~~~>",
            "~~~~~~>",
            "~~~~~>",
            "~~~~>",
            "~~>",
            "~~>",
            "~>",
            ">"
        };
        private readonly string[] bubbles = new[]
        {
            "*o*o*o*o*o*o*o",
            "o*o*o*o*o*o*o*"
        }; 
        
        //Settings
        private readonly enumSetting mode = new enumSetting("Mode", "SNAKE", new[] { "SNAKE","BUBBLES", "RAINBOW" });
        private readonly booleanSetting everyone = new booleanSetting("Everyone", false);
        private readonly sliderSetting delay = new sliderSetting("Delay", 1, 20, 1);
        
        public Username()//TODO create a manager that tracks and ensure each player has a different name to prevent server getting confused.
        {
            base.SetUp("Username", ModuleManager.Category.Player);
            this.moduleSettings.Add(mode);
            this.moduleSettings.Add(everyone);
            this.moduleSettings.Add(delay);
        }

        public override void OnUpdate()
        {
            MultiplayerPlayerInformation multiplayerPlayerInformation = (MultiplayerPlayerInformation) GameObject.Find("Player").GetComponent(typeof(MultiplayerPlayerInformation));
            MultiplayerPlayerScript[] multiplayerPlayerScripts = (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript));
            if (multiplayerPlayerScripts.Length > 0)
            {
                foreach (var clients in multiplayerPlayerScripts)
                {
                    if (!everyone.Value && multiplayerPlayerInformation.myplayer.name != clients.name) continue;
                    if (mode.Selected == "SNAKE")
                    {
                        if (frameCounter >= snake.Length)
                        {
                            frameCounter = 0;
                        }

                        FrameIncrease(snake, clients);
                    }
                    else if (mode.Selected == "BUBBLES")
                    {
                        if (frameCounter >= bubbles.Length)
                        {
                            frameCounter = 0;
                        }

                        FrameIncrease(bubbles, clients);
                    }
                    else
                    {
                        //TODO add rainbow names.
                    }
                }
            }
        }

        private void FrameIncrease(string[] frames, MultiplayerPlayerScript clients)
        {
            if (delayTimer.TimePassed(delay.GetValueFloat() / 100))
            {
                delayTimer.Reset();
                clients.CallCmdSetName(frames[frameCounter], frames[frameCounter]);
                //clients.Networkplayername = frames[frameCounter];
                //clients.Networkplainname = frames[frameCounter];
                frameCounter++;
            }
        }
    }
}