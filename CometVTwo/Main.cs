using CometVTwo.Modules;
using CometVTwo.Modules.Hacks.InGame.Other;
using CometVTwo.Utils;
using CometVTwo.Utils.FileSystem;
using CometVTwo.Utils.Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CometVTwo
{
    public class Main : MonoBehaviour
    {
        //Vars
        public const string version = "0.0.6";
        public new const string name = "CometV2";
        public static string OS;
        private static float FPS = 0;
        private static Color colour = Color.magenta;
        //Managers and shit.
        public static FileManager FileManager = new FileManager();
        public static ModuleManager ModuleManager = new ModuleManager();
        public static BindingHandler BindingHandler = new BindingHandler();
        public static WindowManager WindowManager = new WindowManager();
        private readonly TimeDelay LoadDelay = new TimeDelay();//Don't reset this.
        private readonly TimeDelay CycleDelay = new TimeDelay();

        public void Start()
        {
            OS = SystemInfo.operatingSystem;
            ModuleManager.Init();
            FileManager.SetLog(true);
            FileManager.LoadAll();
            FileManager.Log("Started CometVTwo!");
        }
        public void Update()
        {
            Cycle();
            FPS = Utils.Utils.GetFrameRate();
            if (LoadDelay.TimePassed(8f))
            {
                if (BindingHandler.AreWeBinding())
                {
                    BindingHandler.UpdateBinding();
                }

                ModuleManager.OnKeyPressed();
                ModuleManager.OnUpdate();
            }
        }
        public void OnGUI()
        {
            //Module and MainMenu rendering.
            GUI.color = colour;
            GUI.Label(new Rect(10f, 10f, 4000f, 4000f), 
                string.Format("{0} {1} Menu | Version: {2} | By: 3rd#1703 | OS: {3} | Scene: {4} | FPS: {5}", name, 
                    SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("MainMenu")) ? "MainMenu" : "Ingame", version, OS, SceneManager.GetActiveScene().name, FPS));
            
            ModuleManager.OnGUI();
            WindowManager.OnGUIEnd();
        }

        public void OnPostRender()
        {
            ModuleManager.OnPostRender();
        }

        public void OnPreRender()
        {
            ModuleManager.OnPreRender();
        }

        private void Cycle()//For the pride people.
        {
            if (CycleDelay.TimePassed(ClickMenu.rainbowCycleSpeed.GetValueFloat() / 100))
            {
                CycleDelay.Reset();
                float H, S, V;
                Color.RGBToHSV(colour, out H, out S, out V);
                if (H >= 1.0f)
                {
                    H = 0;
                }
                else
                {
                    H += 0.01f;
                }

                colour = Color.HSVToRGB(H, S, V);
            }
        }
        
        public static Color cycleColour
        {
            get => colour;
        }

        public void OnDestroy()
        {
            Commands.Init();
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