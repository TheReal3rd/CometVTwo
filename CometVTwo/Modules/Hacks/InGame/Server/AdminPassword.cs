using System;
using System.Collections.Generic;
using CometVTwo.Modules.Hacks.MainMenu;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Server
{
    public class AdminPassword : Module
    {
        //Vars
        private Rect windowRect = new Rect(20, 420, 360, 400);
        private Vector2 scrollPosition;
        private List<String> adminPasswordList = new List<String>();
        
        //Settings
        private readonly rectSetting adminWindow = new rectSetting("AdminWindow", new Rect(20, 420, 360, 400));
        private readonly booleanSetting attemptGrab = new booleanSetting("AttemptGrab","Tries to grab the admin password.", false);
        private readonly booleanSetting forceAdmin = new booleanSetting("ForceAdmin", true);
        
        public AdminPassword()
        {
            base.SetUp("GiveAdmin", ModuleManager.Category.Server);
            this.moduleSettings.Add(adminWindow);
            this.moduleSettings.Add(attemptGrab);
            this.moduleSettings.Add(forceAdmin);
        }

        public override void OnUpdate()
        {
            if (forceAdmin.Value)
            {
                MultiplayerPlayerScript[] multiplayerPlayerScripts =
                    (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript));
                foreach (var clients in multiplayerPlayerScripts) //Just set everyone as admin and makes you admin so you can run rcon commands no problem...
                {
                    clients.isAdmin = true;
                    clients.NetworkisAdmin = true;
                }
            }
            if (attemptGrab.Value)//Seems to work but not 100% of the time.
            {
                GameRules[] gameRules = (GameRules[]) UnityEngine.Object.FindObjectsOfType(typeof(GameRules));
                foreach (var gameRule in gameRules)
                {
                    if (!string.IsNullOrEmpty(gameRule.adminpass) && !adminPasswordList.Contains(gameRule.adminpass))
                    {
                        adminPasswordList.Add(gameRule.adminpass);
                    }
                }
            }
        }

        public override void OnGUI()
        {
            if (attemptGrab.Value)
            {
                GUI.color = ClickMenuMainMenu.windowColour.Value;
                windowRect =
                    Main.WindowManager.DrawWindow(windowRect, new GUI.WindowFunction(DrawWindow), "ServerList");
                if (adminWindow.Update)
                {
                    windowRect = adminWindow.Value;
                    adminWindow.Update = false;
                }
                else
                {
                    adminWindow.Value = windowRect;
                }
            }
        }
        
        private void DrawWindow(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(windowRect.width - 15), GUILayout.Height(windowRect.height - 15));
            GUILayout.Label("Passwords:", new GUILayoutOption[0]);
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            foreach (var password in adminPasswordList)
            {
                GUILayout.Label(password, new GUILayoutOption[0]);
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        }

        public override void OnDisable()
        {
            adminPasswordList.Clear();
        }
    }
}