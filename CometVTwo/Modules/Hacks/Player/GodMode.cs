using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.Player
{
    public class GodMode : Module
    {
        //Vars
        private PlayerHealthManagement playerHealthManagement;
        private MultiplayerPlayerInformation multiplayerPlayerInformation;
        private MultiplayerPlayerScript[] MultiplayerPlayerScripts;
        //Settings
        private readonly enumSetting modes =
            new enumSetting("Mode", "BUILTIN", new[] {"BUILTIN", "HEALTH", "SET", "PICKUP", "CLIENT"});
        private readonly doubleSetting health =
            new doubleSetting("Health", 0, 1000, 10, 500);
        private readonly doubleSetting armour =
            new doubleSetting("Armour", 0, 1000, 10, 500);
        
        public GodMode()
        {
            base.SetUp("GodMode", ModuleManager.Category.Player);
            this.moduleSettings.Add(modes);
            this.moduleSettings.Add(health);
            this.moduleSettings.Add(armour);
        }

        public override void OnUpdate()
        {
            playerHealthManagement = (PlayerHealthManagement) GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
            multiplayerPlayerInformation = (MultiplayerPlayerInformation) GameObject.Find("Player").GetComponent(typeof(MultiplayerPlayerInformation));
            MultiplayerPlayerScripts = (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript));
            if (modes.GetSelected() == "BUILTIN")
            {
                BuiltInGodMode();
            }
            else if(modes.GetSelected() == "HEALTH") 
            {
                HealthGodMode();
            }
            else if(modes.GetSelected() == "SET")
            {
                SetPlayerHealth(health.GetValueFloat());
            }
            else if(modes.GetSelected() == "PICKUP")
            {
                if (playerHealthManagement.myhealth < 200f)
                {
                    PlayerPickupScript script = (PlayerPickupScript) GameObject.Find("Player").GetComponent(typeof(PlayerPickupScript));;
                    script.pickupholyhealth(new GameObject());
                }
            }
            else
            {
                foreach (var clients in MultiplayerPlayerScripts)
                {
                    if (clients.name == multiplayerPlayerInformation.myplayer.name)
                    {
                        clients.myhealth = health.GetValueFloat();
                        clients.myarmor = armour.GetValueFloat();
                    }
                }
            }
        }
        
        private void SetPlayerHealth(float health)
        {
            playerHealthManagement.myhealth = health;
        }
        private void HealthGodMode()
        {
            SetPlayerHealth(1000);
        }
        private void BuiltInGodMode()
        {
            if (playerHealthManagement)
            {
                playerHealthManagement.godmode = !playerHealthManagement.godmode;
            }
        }
    }
}