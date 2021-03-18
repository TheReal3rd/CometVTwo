using System.Collections.Generic;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.InGame.Other
{
    public class Tracers : Module
    {
        //Vars
        private static Vector3 playerPos;
        private static List<Vector3> targetPos = new List<Vector3>();
        //Pasted https://github.com/KleskBY/Unity3d-Aimbot-Wallhack-ESP/blob/master/TitaniumExample/Example.cs
        private static Texture2D aaLineTex = null;
        private static Material blendMaterial = null;
        private static Texture2D lineTex = null;
        private static Material blitMaterial = null;
        private static Rect lineRect = new Rect(0f, 0f, 1f, 1f);
        //Settings
        private readonly colorSetting colour = new colorSetting("LineColour", Color.magenta);
        public Tracers()//Not working...
        {
            base.SetUp("Tracers", ModuleManager.Category.Other);
            this.moduleSettings.Add(colour);
        }

        public override void OnUpdate()
        {
            targetPos.Clear();
            playerPos = ((MyControllerScript[])UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0].transform.position;
            MultiplayerPlayerInformation multiplayerPlayerInformation = (MultiplayerPlayerInformation) GameObject.Find("Player").GetComponent(typeof(MultiplayerPlayerInformation));
            foreach (var client in (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript)))
            {
                if (multiplayerPlayerInformation.myplayer.name != client.myplayer.name)
                {
                    targetPos.Add(client.transform.position);
                }
            }
        }

        public override void OnGUI()
        {
            //MyMouseLook myMouseLook = (MyMouseLook) GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
            if (targetPos.Count > 1)
            {
                foreach (var target in targetPos)
                {
                    Vector3 worldToScreen = Camera.main.WorldToScreenPoint(target);
                    if (worldToScreen.z > -1)
                    {
                        DrawLine(playerPos, target, colour.Value, 1f, false);
                    }
                }
            }
        }
        
        //Pasted
        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
        {
            float num = pointB.x - pointA.x;
            float num2 = pointB.y - pointA.y;
            float num3 = Mathf.Sqrt(num * num + num2 * num2);
            if (num3 < 0.001f)
            {
                return;
            }
            Texture2D texture2D;
            if (antiAlias)
            {
                width *= 3f;
                texture2D = aaLineTex;
                Material material = blendMaterial;
            }
            else
            {
                texture2D = lineTex;
                Material material2 = blitMaterial;
            }
            float num4 = width * num2 / num3;
            float num5 = width * num / num3;
            Matrix4x4 identity = Matrix4x4.identity;
            identity.m00 = num;
            identity.m01 = -num4;
            identity.m03 = pointA.x + 0.5f * num4;
            identity.m10 = num2;
            identity.m11 = num5;
            identity.m13 = pointA.y - 0.5f * num5;
            GL.PushMatrix();
            GL.MultMatrix(identity);
            GUI.color = color;
            GUI.DrawTexture(lineRect, texture2D);
            GL.PopMatrix();
        }
    }
}