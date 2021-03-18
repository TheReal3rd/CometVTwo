using System;
using CometVTwo.Modules;
using CometVTwo.Utils.FileSystem;
using UnityEngine;

namespace CometVTwo.Utils
{
    public class Commands
    {
        public static void Init()//Uses Dusk's command system. CBA making more commands. Fine ill make a key bind command for people who del the GUI bind.
        {
            NewBlood.Components.Console.AddCommand("toggle", new Action<string[]>(ToggleCommand));
            NewBlood.Components.Console.AddCommand("players", new Action<string[]>(PlayersCommand));
            NewBlood.Components.Console.AddCommand("bind", new Action<string[]>(BindCommand));
        }

        internal static void ToggleCommand(string[] args)
        {
            if (args.Length < 1 || args.Length >= 2)
            {
                NewBlood.Components.Console.Print(args.Length >= 2 ? "Too many arguments!" : "Insufficient amount of arguments.");
            }
            else
            {
                NewBlood.Components.Console.Print("Toggling... "+args[0].ToLower());
                Module target = Main.ModuleManager.GetModule(args[0]);
                Main.ModuleManager.Toggle(target);
            }
        }

        internal static void PlayersCommand(string[] args)//Dusk's version sometimes fails...
        {
            foreach (var player in (NetworkPlayer[]) UnityEngine.Object.FindObjectsOfType(typeof(NetworkPlayer)))
            {
                NewBlood.Components.Console.Print("Name: "+player.igName+"  Steam: "+player.steamName+"  SID: "+player.steamId);
            }
        }

        internal static void BindCommand(string[] args)//Example "bind autowin pageup" bind <module> <key>
        {
            if (args.Length < 1 || args.Length >= 3)
            {
                NewBlood.Components.Console.Print( args.Length >= 3 ? "Too many arguments" : "Insufficient amount of arguments");
            } 
            else if(args[0] == "help" && string.IsNullOrEmpty(args[1])) 
            {
                NewBlood.Components.Console.Print("Bind help: bind <module> <key>\n To remove bind use keycode none");
            }
            else
            {
                KeyCode newBind = FileManager.StringToKeyCode(args[1]);
                Module targetModule = Main.ModuleManager.GetModule(args[0]);
                if (!newBind.Equals(null) && !targetModule.Equals(null))
                {
                    targetModule.bind.Bind = newBind;
                }
                else
                {
                    NewBlood.Components.Console.Print("An error occured please check you entries.");
                }
            }
        }
    }
}