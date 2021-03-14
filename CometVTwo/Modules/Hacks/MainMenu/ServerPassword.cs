using System;
using System.Collections.Generic;
using CometVTwo.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace CometVTwo.Modules.Hacks.MainMenu
{
    public class ServerPassword : Module//Big password system.
    {
        //Vars
        private List<Pair<String, String>> passwords = new List<Pair<String, String>>();
        private string targetPassword;
        private MapManager[] MapManager;
        private Rect windowRect = new Rect(20, 420, 360, 400);
        private Vector2 scrollPosition;

        public ServerPassword()
        {
            base.SetUp("ServerPassword", ModuleManager.Category.MainMenu);
        }

        public override void OnUpdate()
        {
            MapManager = (MapManager[]) UnityEngine.Object.FindObjectsOfType(typeof(MapManager));
            foreach (MapManager map in MapManager)
            {
                Pair<String,String> info = new Pair<String, String>(map.servername, map.joinpass);
                if ((map.joinpass != "" || map.joinpass == null) && !Contains(info))
                {
                    passwords.Add(info);
                }
            }
            
            if (!String.IsNullOrEmpty(targetPassword))
            {
                GameObject.Find("PassName").GetComponent<InputField>().text = targetPassword;
            }
        }

        public override void OnGUI()
        {
            GUI.color = ClickMenuMainMenu.windowColour.Value;
            windowRect = GUI.Window(2, windowRect, new GUI.WindowFunction(DrawWindow), "PasswordList");
        }

        private void DrawWindow(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(windowRect.width - 15), GUILayout.Height(windowRect.height - 15));
            GUILayout.Label("Passwords:", new GUILayoutOption[0]);
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            foreach (Pair<String,String> password in passwords)
            {
                if (GUILayout.Button(password.First+" | Pass: "+password.Second, new GUILayoutOption[0]))
                {
                    targetPassword = password.Second;
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        }

        public override void OnDisable()
        {
            passwords.Clear();
        }

        public bool Contains(Pair<String,String> input)
        {
            foreach (var password in passwords)
            {
                if (password.First == input.First)
                {
                    return true;
                }
            }

            return false;
        }
    }
}