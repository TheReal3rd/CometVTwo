using System;
using System.Collections.Generic;
using CometVTwo.Modules.Hacks.InGame.Other;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Server
{
    public class ServerInfo : Module
    {
        //Vars
        private Rect windowRect = new Rect(20, 420, 200, 400);
        private List<String> infoList = new List<String>();
        //Settings
        private readonly booleanSetting playerCount = new booleanSetting("PlayerCount", true);
        private readonly booleanSetting readyPlayers = new booleanSetting("ReadyPlayers", true);
        private readonly booleanSetting serverName = new booleanSetting("ServerName", true);
        private readonly booleanSetting currentMap = new booleanSetting("CurrentMap", true);
        private readonly rectSetting serverInfoRect = new rectSetting("ServerInfo", new Rect(20, 420, 200, 400));
        
        public ServerInfo()
        {
            base.SetUp("ServerInfo", ModuleManager.Category.Server);
            this.moduleSettings.Add(playerCount);
            this.moduleSettings.Add(readyPlayers);
            this.moduleSettings.Add(serverName);
            this.moduleSettings.Add(currentMap);
            this.moduleSettings.Add(serverInfoRect);
        }

        public override void OnUpdate()
        {
            GameRules gameRules = (GameRules) UnityEngine.Object.FindObjectsOfType(typeof(GameRules))[0];
            infoList.Clear();
            if (playerCount.Value && !gameRules.Equals(null))
            {
                string count = String.Format("PlayerCount: {0}/{1}", gameRules.numplayers, gameRules.maxplayers);
                infoList.Add(count);
            }
            if (readyPlayers.Value && !gameRules.Equals(null))
            {
                string ready = String.Format("ReadyPlayers: {0}/{1}", gameRules.readyplayers, gameRules.numplayers);
                infoList.Add(ready);
            }
            if (serverName.Value && !String.IsNullOrEmpty(gameRules.servername))
            {
                infoList.Add("ServerName: " + gameRules.servername.Substring(0, 15));
            }
            if (currentMap.Value && !String.IsNullOrEmpty(gameRules.currentMap))
            {
                infoList.Add("MapName: "+gameRules.currentMap);
            }
            
            windowRect.height = GenerateHeight();
        }

        public override void OnGUI()
        {
            GUI.color = ClickMenu.serverColour.Value;
            windowRect = Main.WindowManager.DrawWindow(windowRect, new GUI.WindowFunction(DrawWindow), "ServerInfo");
            if (serverInfoRect.Update)
            {
                windowRect = serverInfoRect.Value;
                serverInfoRect.Update = false;
            }
            else
            {
                serverInfoRect.Value = windowRect;
            }
        }
        
        private void DrawWindow(int windowID)
        {
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            if (infoList.Count > 0)
            {
                foreach (var info in infoList)
                {
                    GUILayout.Label(info, new GUILayoutOption[0]);
                }
            }

            GUILayout.EndVertical();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        } 
        
        private int GenerateHeight()
        {
            return (infoList.Count * 25) + 16;
        }
    }
}