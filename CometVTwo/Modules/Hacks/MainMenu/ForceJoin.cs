using System;
using System.Collections.Generic;
using CometVTwo.Utils;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

namespace CometVTwo.Modules.Hacks.MainMenu
{
    public class ForceJoin : Module//Join full and password protected servers.
    {
        private MapManager[] MapManager;
        private ServerEntryScript[] serverEntryScript;
        private Rect windowRect = new Rect(20, 420, 360, 400);
        private Vector2 scrollPosition;
        private List<Quad<String, String, String, CSteamID>> serverList = new List<Quad<String, String, String, CSteamID>>();

        public ForceJoin()
        {
            base.SetUp("ForceJoin", ModuleManager.Category.MainMenu);
        }

        public override void OnUpdate()
        {
            MapManager = (MapManager[]) UnityEngine.Object.FindObjectsOfType(typeof(MapManager));
            serverEntryScript = (ServerEntryScript[]) UnityEngine.Object.FindObjectsOfType(typeof(ServerEntryScript));
            foreach (ServerEntryScript servers in serverEntryScript)
            {
                if (servers.full)
                {
                    servers.full = false;//Its full no more!
                }
            }

            foreach (MapManager map in MapManager)
            {
                Quad<String, String, String, CSteamID> info =
                    new Quad<String, String, String, CSteamID>(map.servername, map.joinpass, map.mapname,
                        map.lobbynumber);
                if ((map.ipaddr != "" || map.ipaddr != null) && !Contains(info) && map.type == 1)
                {
                    serverList.Add(info);
                }
            }
        }

        public override void OnGUI()
        {
            GUI.color = ClickMenuMainMenu.windowColour.Value;
            windowRect = GUI.Window(3, windowRect, new GUI.WindowFunction(DrawWindow), "ServerList");
        }

        private void DrawWindow(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(windowRect.width - 15), GUILayout.Height(windowRect.height - 15));
            GUILayout.Label("Servers:", new GUILayoutOption[0]);
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            foreach (var server in serverList)
            {
                if (GUILayout.Button(String.Format("Name: {0} | Map: {1} ", server.First, server.Third), new GUILayoutOption[0]))
                {
                    GameObject.Find("PassName").GetComponent<InputField>().text = "";
                    MapManager[0].type = 1;
                    MapManager[0].joinpass = "";
                    MapManager[0].mapname = server.Third;
                    MapManager[0].lobbynumber = server.Fourth;
                    MapManager[0].maxplayers = 255;
                    MapManager[0].JoinGame();
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        }
        
        public override void OnDisable()
        {
            serverList.Clear();
        }
        
        public bool Contains(Quad<String, String, String, CSteamID> input)
        {
            foreach (var server in serverList)
            {
                if (server.First == input.First)
                {
                    return true;
                }
            }

            return false;
        }
    }
}