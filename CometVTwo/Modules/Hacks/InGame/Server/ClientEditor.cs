using System;
using System.Collections.Generic;
using CometVTwo.Modules.Hacks.InGame.Movement;
using CometVTwo.Modules.Hacks.InGame.Other;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Server
{
    public class ClientEditor : Module
    {
        //Vars
        private Rect windowRect = new Rect(20, 420, 360, 400);
        private Vector2 scrollPosition;
        private MultiplayerPlayerScript[] multiplayerPlayerScript;
        private MultiplayerPlayerScript target;//The player we're editing change to null when we're not or done editing.
        private string newName = "Name";
        private int newKills = 0;
        private int newDeaths = 0;
        private int newHealth = 0;
        private int newArmour = 0;
        private List<MultiplayerPlayerScript> godModeList = new List<MultiplayerPlayerScript>();//All the players with godMode.
        //Settings
        private readonly rectSetting windowPosition = new rectSetting("ClientEditor", new Rect(20, 420, 360, 400));
            
        public ClientEditor()
        {
            base.SetUp("ClientEditor", ModuleManager.Category.Server);
            this.moduleSettings.Add(windowPosition);
        }

        public override void OnUpdate()
        {
            multiplayerPlayerScript = (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript));
            if (godModeList.Count > 0)
            {
                foreach (var playerScript in godModeList)
                {
                    playerScript.myhealth = 1000f;
                }
            }
        }

        public override void OnGUI()
        {
            GUI.color = ClickMenu.serverColour.Value;
            windowRect = Main.WindowManager.DrawWindow(windowRect, new GUI.WindowFunction(DrawWindow), "Clients");
            if (windowPosition.Update)
            {
                windowRect = windowPosition.Value;
                windowPosition.Update = false;
            }
            else
            {
                windowPosition.Value = windowRect;
            }
        }

        private void DrawWindow(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(windowRect.width - 15), GUILayout.Height(windowRect.height - 15));
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            if (target == null)
            {
                GUILayout.Label("Players:", new GUILayoutOption[0]);
                foreach (MultiplayerPlayerScript players in multiplayerPlayerScript)
                {
                    if (players != null)
                    {
                        if (GUILayout.Button(players.playername, new GUILayoutOption[0]))
                        {
                            target = players;
                        }
                    }
                }
            }
            else
            {
                GUILayout.Label(target.playername, new GUILayoutOption[0]);
                //Name
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                newName = GUILayout.TextField(newName, new GUILayoutOption[0]);
                if (GUILayout.Button("Apply", new GUILayoutOption[0]))
                {
                    //target.plainname = newName;
                    //target.playername = newName;
                    //target.username = newName;
                    target.CmdSetName(newName, newName);
                }
                GUILayout.EndHorizontal();
                //Kills
                GUILayout.Label("Kills:", new GUILayoutOption[0]);
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                newKills = Int32.Parse(GUILayout.TextField(newKills.ToString(), new GUILayoutOption[0]));
                if (GUILayout.Button("Apply", new GUILayoutOption[0]))
                {
                    target.kills = newKills;
                }
                GUILayout.EndHorizontal();
                //Deaths
                GUILayout.Label("Deaths:", new GUILayoutOption[0]);
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                newDeaths = Int32.Parse(GUILayout.TextField(newDeaths.ToString(), new GUILayoutOption[0]));
                if (GUILayout.Button("Apply", new GUILayoutOption[0]))
                {
                    target.deaths = newDeaths;
                }
                GUILayout.EndHorizontal();
                //TP To
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                if (GUILayout.Button("TeleportTo", new GUILayoutOption[0]))
                {
                    if (Main.ModuleManager.IsModuleActive(Main.ModuleManager.GetModule("NoClip")))
                    {
                        NoClip.PlayerPos = target.transform.position;
                    }
                    else
                    {
                        ((MyControllerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0]
                            .transform.position = target.transform.position;
                    }
                }
                if (GUILayout.Button("TeleportThem", new GUILayoutOption[0]))
                {
                    target.transform.position = ((MyControllerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0]
                        .transform.position;
                }
                GUILayout.EndHorizontal();
                if (!godModeList.Contains(target))
                {
                    if (GUILayout.Button("GiveGodMode", new GUILayoutOption[0]))
                    {
                        godModeList.Add(target);
                    }
                }
                else
                {
                    if (GUILayout.Button("RemoveGodMode", new GUILayoutOption[0]))
                    {
                        godModeList.Remove(target);
                    }
                }
                GUILayout.Label("Health:", new GUILayoutOption[0]);
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                //Health
                newHealth = Int32.Parse(GUILayout.TextField(newHealth.ToString(), new GUILayoutOption[0]));
                if (GUILayout.Button("Apply", new GUILayoutOption[0]))
                {
                    target.myhealth = newHealth;
                }
                GUILayout.EndHorizontal();
                GUILayout.Label("Armour:", new GUILayoutOption[0]);
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                //Armour
                newArmour = Int32.Parse(GUILayout.TextField(newArmour.ToString(), new GUILayoutOption[0]));
                if (GUILayout.Button("Apply", new GUILayoutOption[0]))
                {
                    target.myarmor = newArmour;
                }
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Kick", new GUILayoutOption[0]))
                {
                    target.kickbool = true;
                }
                if (GUILayout.Button("Done", new GUILayoutOption[0]))
                {
                    target = null;
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        }
    }
}