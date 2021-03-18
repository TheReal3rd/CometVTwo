using CometVTwo.Settings;
using CometVTwo.Utils.Objects;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Player
{
    public class GodMode : Module
    {
        //Vars
        private PlayerHealthManagement playerHealthManagement;
        private GameObject fakeHealth;
        //Settings
        private static readonly enumSetting modes = new enumSetting("Mode", "BUILTIN", new[] {"BUILTIN", "PICKUP", "CLIENT", "CLIENTPICKUP"});
        private readonly doubleSetting health = new doubleSetting("Health", 0, 1000, 10, 500);
        private readonly doubleSetting armour = new doubleSetting("Armour", 0, 1000, 10, 500);

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
            MultiplayerPlayerInformation multiplayerPlayerInformation = (MultiplayerPlayerInformation) GameObject.Find("Player").GetComponent(typeof(MultiplayerPlayerInformation));
            MultiplayerPlayerScript[]  multiplayerPlayerScripts = (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript));
            switch (modes.Selected)
            {
                case "BUILTIN"://Shitty built in godmode when health reaches 0 and below you go invisible.
                    if (!playerHealthManagement.Equals(null))
                    {
                        playerHealthManagement.godmode = true;
                    }
                    break;
                case "PICKUP"://Now works when none host. Just annoying with its sound and stuff.
                    if (!playerHealthManagement.Equals(null) && playerHealthManagement.myhealth < 200f)
                    {
                        PlayerPickupScript script = (PlayerPickupScript) GameObject.Find("Player").GetComponent(typeof(PlayerPickupScript));
                        if (!script.Equals(null))
                        {
                            fakeHealth = new GameObject();
                            fakeHealth.AddComponent<DummyClass>();
                            fakeHealth.tag = "HolyHealthTag";
                            fakeHealth.transform.position = multiplayerPlayerInformation.myplayer.transform.position;
                            script.pickupholyhealth(fakeHealth);
                        }
                    }
                    break;
                case "CLIENT"://Works only when host.
                    if (multiplayerPlayerScripts.Length > 0)
                    {
                        foreach (var clients in multiplayerPlayerScripts)
                        {
                            if (clients.name == multiplayerPlayerInformation.myplayer.name)
                            {
                                clients.myhealth = health.GetValueFloat();
                                clients.myarmor = armour.GetValueFloat();
                            }
                        }
                    }
                    break;
                case "CLIENTPICKUP"://Works none host and no annoying sounds plus other players can't hear it.
                    if (multiplayerPlayerScripts.Length > 0 && !playerHealthManagement.Equals(null) && playerHealthManagement.myhealth < 200f)
                    {
                        foreach (var clients in multiplayerPlayerScripts)
                        {
                            if (clients.name == multiplayerPlayerInformation.myplayer.name)
                            {
                                clients.CallCmdSetHealth(health.GetValueFloat(), armour.GetValueFloat());
                            }
                        }
                    }
                    break;
            }
        }

        public override void OnDisable()
        {
            if (!playerHealthManagement.Equals(null) && playerHealthManagement.godmode)
            {
                playerHealthManagement.godmode = false;
            }
        }
    }
}