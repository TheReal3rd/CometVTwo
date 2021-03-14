using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Server
{
    public class AdminPassword : Module//Originally aimed to crack and grab the Dedicated servers passwords to then use "rcon login" command.
    {
        //Vars
        private Vector2 scrollPosition;
        private MultiplayerPlayerScript[] multiplayerPlayerScripts;

        public AdminPassword()
        {
            base.SetUp("GiveAdmin", ModuleManager.Category.Server);
        }

        public override void OnUpdate()
        {
            multiplayerPlayerScripts = (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript));
            //playerlist command doesn't work. Use players command instead to get steam info.
            foreach (var clients in multiplayerPlayerScripts)//Just set everyone as admin and makes you admin so you can run rcon commands no problem...
            {
                clients.isAdmin = true;
                clients.NetworkisAdmin = true;
            }
        }
    }
}