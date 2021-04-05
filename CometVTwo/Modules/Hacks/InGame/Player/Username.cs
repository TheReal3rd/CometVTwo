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
        private readonly enumSetting mode = new enumSetting("Mode", "SNAKE", new[] {"SNAKE","BUBBLES","RAINBOW","SET"});
        private readonly booleanSetting everyone = new booleanSetting("Everyone", false);
        private readonly sliderSetting delay = new sliderSetting("Delay", 1, 20, 1);
        private readonly stringSetting setName = new stringSetting("SetName", "Username");
        
        public Username()//TODO create a manager that tracks and ensure each player has a different name to prevent server getting confused.
        {
            base.SetUp("Username", ModuleManager.Category.Player);
            this.moduleSettings.Add(mode);
            this.moduleSettings.Add(everyone);
            this.moduleSettings.Add(delay);
            this.moduleSettings.Add(setName);
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
                    switch (mode.Selected)
                    {
                        case "SNAKE":
                            if (frameCounter >= snake.Length)
                            {
                                frameCounter = 0;
                            }
                            FrameIncrease(snake, clients);
                            break;
                        case "BUBBLES":
                            if (frameCounter >= bubbles.Length)
                            {
                                frameCounter = 0;
                            }
                            FrameIncrease(bubbles, clients);
                            break;
                        case "RAINBOW":
                            //TODO make rainbow names.
                            break;
                        case "SET":
                            clients.Networkplayername = setName.Value;
                            clients.Networkplainname = setName.Value;
                            break;
                    }
                }
            }
        }

        public override void SlowUpdate()
        {
            setName.Visible = mode.Selected == "SET";
            delay.Visible = mode.Selected != "SET";
        }

        private void FrameIncrease(string[] frames, MultiplayerPlayerScript clients)
        {
            if (delayTimer.TimePassed(delay.GetValueFloat() / 100))
            {
                delayTimer.Reset();
                clients.Networkplayername = frames[frameCounter];
                clients.Networkplainname = frames[frameCounter];
                frameCounter++;
            }
        }
    }
}