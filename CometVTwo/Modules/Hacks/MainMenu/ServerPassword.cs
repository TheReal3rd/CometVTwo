using System;
using System.Collections.Generic;
using System.Linq;
using CometVTwo.Settings;
using CometVTwo.Utils.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace CometVTwo.Modules.Hacks.MainMenu
{
    public class ServerPassword : Module//Big password system.
    {
        //Vars
        private string targetPassword;
        private MapManager[] MapManager;
        private Rect windowRect = new Rect(20, 420, 360, 400);
        private Vector2 scrollPosition;
        private List<Pair<String,String>> passwordList = new List<Pair<String,String>>();
        
        //Settings
        private readonly rectSetting passwordRect = new rectSetting("WindowPos", new Rect(20, 420, 360, 400));
        
        public ServerPassword()
        {
            base.SetUp("ServerPassword", ModuleManager.Category.MainMenu);
            this.moduleSettings.Add(passwordRect);
        }

        public override void OnUpdate()
        {
            MapManager = (MapManager[]) UnityEngine.Object.FindObjectsOfType(typeof(MapManager));
            if (MapManager.Length > 0)
            {
                foreach (MapManager server in MapManager)
                {
                    Pair<String, String> info = new Pair<String, String>(server.servername, server.joinpass);
                    if (!Contains(info) && !String.IsNullOrEmpty(server.joinpass))
                    {
                        passwordList.Add(info);
                    }
                }
            }
            if (!String.IsNullOrEmpty(targetPassword))
            {
                GameObject.Find("PassName").GetComponent<InputField>().text = targetPassword;
                targetPassword = null;
            }
        }

        public override void OnGUI()
        {
            GUI.color = ClickMenuMainMenu.windowColour.Value;
            windowRect = Main.WindowManager.DrawWindow(windowRect, new GUI.WindowFunction(DrawWindow), "PasswordList");
            if (passwordRect.Update)
            {
                windowRect = passwordRect.Value;
                passwordRect.Update = false;
            }
            else
            {
                passwordRect.Value = windowRect;
            }
        }

        private void DrawWindow(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(windowRect.width - 15), GUILayout.Height(windowRect.height - 15));
            GUILayout.Label("Passwords:", new GUILayoutOption[0]);
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            foreach (Pair<String,String> server in passwordList)
            {
                if (!string.IsNullOrEmpty(server.Second))
                {
                    if (GUILayout.Button(server.First + " | Pass: " + server.Second, new GUILayoutOption[0]))
                    {
                        targetPassword = server.Second;
                    }
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        }

        private bool Contains(Pair<String,String> data)
        {
            foreach (var entries in passwordList)
            {
                if (entries.Second == data.Second)
                {
                    return true;
                }
            }

            return false;
        }

        public override void OnDisable()
        {
            passwordList.Clear();
        }
    }
}