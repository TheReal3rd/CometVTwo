using System;
using System.Collections.Generic;
using UnityEngine;

namespace CometVTwo.Utils
{
    public class ServerUtils
    {
        public static GameRules[] GameRulesArray = (GameRules[]) UnityEngine.Object.FindObjectsOfType(typeof(GameRules));
        public static MultiplayerPlayerScript[] MultiplayerPlayerScripts = (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript));

        public static void SetClientHealth(int client, int health)
        {
            ((MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript)))[client]
                .Networkmyhealth = health;
        }
        public static void SetClientPosition(int client, Vector3 vector3)
        {
            ((MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript)))[client]
                .NetworksyncPos = vector3;
        }

        public static string[] GetPLayerList()
        {
            List<String> stringList = new List<string>();
            for (int i = 0; i != ((MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript))).Length; i++)
            {
                stringList.Add(((MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript)))[i].playername);
            }
            return stringList.ToArray();
        }
    }
}