using System;
using CometVTwo.Settings;
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
            foreach (MapManager server in MapManager)
            {
                if (!string.IsNullOrEmpty(server.joinpass))
                {
                    if (GUILayout.Button(server.servername + " | Pass: " + server.joinpass, new GUILayoutOption[0]))
                    {
                        targetPassword = server.joinpass;
                    }
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        }
    }
}