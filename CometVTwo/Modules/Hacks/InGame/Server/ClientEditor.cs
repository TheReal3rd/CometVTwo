using CometVTwo.Modules.Hacks.Other;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Server
{
    public class ClientEditor : Module
    {
        //Vars
        private Rect windowRect = new Rect(20, 420, 360, 400);
        private Vector2 scrollPosition;
        
        public ClientEditor()
        {
            base.SetUp("ClientEditor", ModuleManager.Category.Server);
        }

        public override void OnGUI()
        {
            GUI.color = ClickMenu.serverColour.GetValue();
            windowRect = GUI.Window(6, windowRect, new GUI.WindowFunction(DrawWindow), "Clients");
        }

        private void DrawWindow(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(windowRect.width - 15), GUILayout.Height(windowRect.height - 15));
            GUILayout.Label("Players:", new GUILayoutOption[0]);
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            foreach (MultiplayerPlayerScript client in (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript)))
            {
                if (client != null && client.locplainname != null)
                {
                    GUILayout.Label(client.locplainname, new GUILayoutOption[0]);
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        }
    }
}