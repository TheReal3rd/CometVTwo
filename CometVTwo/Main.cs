using CometVTwo.Modules;
using CometVTwo.Utils;
using CometVTwo.Utils.FileSystem;
using UnityEngine;

namespace CometVTwo
{
    public class Main : MonoBehaviour//TODO need to make a window ID manager.
    {
        //Vars
        public const string version = "0.0.3";
        public const string name = "CometV2";
        public static string OS;
        private static Color colour;
        private int[] rgb = new []{ 0, 0, 0 };
        private bool reverse = false;
        //Managers and shit.
        public static FileManager FileManager = new FileManager();
        public static ModuleManager ModuleManager = new ModuleManager();
        public static BindingHandler BindingHandler = new BindingHandler();

        public void Start()
        {
            InvokeRepeating("Cycle", 0.5f,0.01f);
            OS = SystemInfo.operatingSystem;
            ModuleManager.Init();
            FileManager.LoadAll();
            FileManager.SetLog(false);
            FileManager.Log("Started CometVTwo!");
            //HarmonyUtil.DoPatching(); TODO look into harmony more to see if we can use it to replace Dusk's Methods
            //From the looks of it, harmony code must be in the library and acts as an interface so meaning we must edit the games libs.
            //I don't want to do that cause that could be detected by an AntiCheat or AntiTampering System. 
        }
        public void Update()
        {
            if (BindingHandler.AreWeBinding())
            {
                BindingHandler.UpdateBinding();
            }
            ModuleManager.OnKeyPressed();
            ModuleManager.OnUpdate();
        }
        public void OnGUI()
        {
            //Module and MainMenu rendering.
            if (Application.loadedLevelName == "MainMenu")
            {
                GUI.color = colour;
                GUI.Label(new Rect(10f, 10f, 4000f, 4000f), string.Format("{0} MainMenu Menu | Version: {1} | By: 3rd#1703 | OS: {2}", name, version, OS));
            }
            else
            {
                GUI.color = colour;
                GUI.Label(new Rect(10f, 10f, 4000f, 4000f), string.Format("{0} Ingame Menu | Version: {1} | By: 3rd#1703 | OS: {2}", name, version, OS));
            }
            ModuleManager.OnGUI();
        }

        public void Cycle()//TODO make one that's better.
        {
            if (!reverse)
            {
                if (rgb[0] != 255)
                {
                    rgb[0]++;
                }
                else if (rgb[1] != 255)
                {
                    rgb[1]++;
                }
                else if (rgb[2] != 255)
                {
                    rgb[2]++;
                }
                else
                {
                    reverse = !reverse;
                }
            }
            else
            {
                if (rgb[0] != 0)
                {
                    rgb[0]--;
                }
                else if (rgb[1] != 0)
                {
                    rgb[1]--;
                }
                else if (rgb[2] != 100)
                {
                    rgb[2]--;
                }
                else
                {
                    reverse = !reverse;
                }
            }

            colour = Utils.Utils.RGBToColour(rgb);
        }
        
        public static Color cycleColour
        {
            get => colour;
        }

        public void OnDestroy()
        {
            FileManager.SaveAll();
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