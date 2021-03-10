using CometVTwo.menu;
using CometVTwo.Modules;
using UnityEngine;

namespace CometVTwo
{
    public class Main : MonoBehaviour
    {
        public const string version = "0.0.1";
        public const string name = "CometV2";
        public static ModuleManager ModuleManager = new ModuleManager();
        public static ClickGuiMain ClickGuiMain = new ClickGuiMain();
        public void Start()
        {          
            ModuleManager.Init();
            ClickGuiMain.Init();
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                ClickGuiMain.Toggle();
            }

            if (Application.loadedLevelName != "MainMenu")
            {
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
                GUI.Label(new Rect(10f, 10f, 4000f, 4000f), name+" MainMenu Menu | Version "+version+" | By: 3rd#1703");
            }
            else
            {
                GUI.color = Color.magenta;
                GUI.Label(new Rect(10f, 10f, 4000f, 4000f), name+" InGame Menu | Version "+version+" | By: 3rd#1703");
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