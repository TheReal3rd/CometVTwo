using CometVTwo.menu;
using CometVTwo.Modules;
using CometVTwo.Utils;
using CometVTwo.Utils.FileSystem;
using UnityEngine;

namespace CometVTwo
{
    public class Main : MonoBehaviour
    {
        //Vars
        public const string version = "0.0.1";
        public const string name = "CometV2";
        public static string OS;
        //Managers and shit.
        public static FileManager FileManager = new FileManager();
        public static ModuleManager ModuleManager = new ModuleManager();
        public static ClickGuiMain ClickGuiMain = new ClickGuiMain();
        
        public void Start()
        {
            OS = SystemInfo.operatingSystem;
            ModuleManager.Init();
            ClickGuiMain.Init();
        }
        public void Update()
        {
            if (Application.loadedLevelName != "MainMenu")
            {
                if (Input.GetKeyDown(KeyCode.Insert))
                {
                    ClickGuiMain.Toggle();
                }
                ModuleManager.OnKeyPressed();
                ModuleManager.OnUpdate();
            }
        }
        public void OnGUI()
        {
            //Module and MainMenu rendering.
            if (Application.loadedLevelName == "MainMenu")
            {
                GUI.color = Color.magenta;
                GUI.Label(new Rect(10f, 10f, 4000f, 4000f), string.Format("{0} MainMenu Menu | Version: {1} | By: 3rd#1703 | OS: {2}", name, version, OS));
            }
            else
            {
                GUI.color = Color.magenta;
                GUI.Label(new Rect(10f, 10f, 4000f, 4000f), string.Format("{0} Ingame Menu | Version: {1} | By: 3rd#1703 | OS: {2}", name, version, OS));
                ModuleManager.OnGUI();
            }
            //Click GUI rendering.
            if (ClickGuiMain.MenuOpen)
            {
                ClickGuiMain.OnGUI();
            }
        }
        
        public void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(this);
            if (UnityEngine.Object.FindObjectsOfType(base.GetType()).Length > 1)
            {
                UnityEngine.Object.Destroy(base.gameObject);
            }
        }
    }
}