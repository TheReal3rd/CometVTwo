using System;
using System.Collections.Generic;
using CometVTwo.Modules.Hacks.InGame.Other;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Server
{
    public class AdminPassword : Module
    {
        //Vars
        private Rect windowRect = new Rect(20, 420, 360, 400);
        private Vector2 scrollPosition;
        private GameRules[] gameRulesArray;
        private List<String> passwordList = new List<String>();
        
        public AdminPassword()
        {
            //Not fully tested need to wait for a Server to pop up.
            base.SetUp("AdminPassword", ModuleManager.Category.Server);
        }

        public override void OnUpdate()
        {
            gameRulesArray = (GameRules[]) UnityEngine.Object.FindObjectsOfType(typeof(GameRules));
            if (gameRulesArray.Length > 0)
            {
                foreach (var game in gameRulesArray)
                {
                    if (game != null && game.adminpass != null && game.adminpass != "" && !passwordList.Contains(game.adminpass))
                    {
                        passwordList.Add(game.adminpass);
                    }
                }
            }
        }
        
        public override void OnGUI()
        {
            GUI.color = ClickMenu.serverColour.Value;
            windowRect = GUI.Window(6, windowRect, new GUI.WindowFunction(DrawWindow), "AdminPasswords");
        }

        public override void OnDisable()
        {
            passwordList.Clear();
        }

        private void DrawWindow(int windowID)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(windowRect.width - 15), GUILayout.Height(windowRect.height - 15));
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            foreach (var password in passwordList)
            {
                GUILayout.Label(password, new GUILayoutOption[0]);
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUI.DragWindow(new Rect(0,0,1000,1000));
        }
    }
}