using System;
using CometVTwo.Modules;

namespace CometVTwo.Utils
{
    public class Commands
    {
        public static void Init()//Uses Dusk's command system. CBA making more commands.
        {
            NewBlood.Components.Console.AddCommand("toggle", new Action<string[]>(ToggleCommand));
            NewBlood.Components.Console.AddCommand("players", new Action<string[]>(PlayersCommand));
        }

        internal static void ToggleCommand(string[] args)
        {
            if (args.Length < 1 || args.Length >= 2)
            {
                NewBlood.Components.Console.Print(args.Length >= 2 ? "Too many module names!" : "Need to specify Module name.");
            }
            else
            {
                NewBlood.Components.Console.Print("Toggling... "+args[0]);
                Module target = Main.ModuleManager.GetModule(args[0]);
                Main.ModuleManager.Toggle(target);
            }
        }

        internal static void PlayersCommand(string[] args)
        {
            foreach (var player in (NetworkPlayer[]) UnityEngine.Object.FindObjectsOfType(typeof(NetworkPlayer)))
            {
                NewBlood.Components.Console.Print("Name: "+player.igName+"  Steam: "+player.steamName+"  SID: "+player.steamId);
            }
        }
    }
}