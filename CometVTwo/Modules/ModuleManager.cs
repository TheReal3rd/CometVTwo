using System;
using System.Collections.Generic;
using CometVTwo.Modules.Hacks.InGame.Movement;
using CometVTwo.Modules.Hacks.InGame.Other;
using CometVTwo.Modules.Hacks.InGame.Player;
using CometVTwo.Modules.Hacks.InGame.Server;
using CometVTwo.Modules.Hacks.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CometVTwo.Modules
{
    /// <summary>
    /// The command and control for the modules.
    /// </summary>
    public class ModuleManager
    {
        public List<Module> modulesList = new List<Module>();

        public void Init()
        {
            //InGame-
            //Other
            modulesList.Add(new ClickMenu());
            modulesList.Add(new Aimbot());
            modulesList.Add(new CrossHair());
            modulesList.Add(new ActiveModules());
            modulesList.Add(new PlayerColour());
            modulesList.Add(new Tracers());
            //Player
            modulesList.Add(new GiveAll());
            modulesList.Add(new GodMode());
            modulesList.Add(new RapidFire());
            modulesList.Add(new UnlimitedAmmo());
            modulesList.Add(new Projectile());
            modulesList.Add(new AutoWin());
            modulesList.Add(new Username());
            //Movement
            modulesList.Add(new JumpModifier());
            modulesList.Add(new UseAmphetamineSalts());
            modulesList.Add(new NoClip());
            modulesList.Add(new Speed());
            modulesList.Add(new ClimbAnything());
            //Server
            modulesList.Add(new MapChanger());
            modulesList.Add(new ClientEditor());
            modulesList.Add(new AdminPassword());
            modulesList.Add(new SpoofSteam());
            modulesList.Add(new ServerInfo());
            //Hidden
            //MainMenu-
            modulesList.Add(new ClickMenuMainMenu());
            modulesList.Add(new ServerPassword());
            modulesList.Add(new ForceJoin());
            modulesList.Add(new CreateServer());
        }

        public void OnUpdate()
        {
            foreach (Module module in modulesList)
            {
                bool mainMenu = SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("MainMenu"));
                if (module.getCategory().Equals(Category.MainMenu) && !mainMenu
                     || !module.getCategory().Equals(Category.MainMenu) && mainMenu)
                {
                    continue;
                }
                if (module.enabled.Value)
                {
                    try
                    {
                        module.OnUpdate();
                    }
                    catch (Exception e)
                    {
                        Main.FileManager.Log(string.Format("OnUpdate {0} Error:\n\n{1},\n\n", module.getName(), e.ToString()));
                    }
                }
            }
        }

        public void OnGUI() {
            foreach (Module module in modulesList)
            {
                bool mainMenu = SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("MainMenu"));
                if (module.getCategory().Equals(Category.MainMenu) && !mainMenu
                    || !module.getCategory().Equals(Category.MainMenu) && mainMenu)
                {
                    continue;
                }
                if (module.enabled.Value)
                {
                    try {
                        module.OnGUI();
                    }
                    catch (Exception e)
                    {
                        Main.FileManager.Log(string.Format("OnGUI {0} Error:\n\n{1},\n\n", module.getName(), e.ToString()));
                    }
                }
            }
        }

        public void OnPostRender()
        {
            foreach (Module module in modulesList)
            {
                bool mainMenu = SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("MainMenu"));
                if (module.getCategory().Equals(Category.MainMenu) && !mainMenu
                    || !module.getCategory().Equals(Category.MainMenu) && mainMenu)
                {
                    continue;
                }
                if (module.enabled.Value)
                {
                    module.OnPostRender();
                }
            }
        }

        public void OnPreRender()
        {
            foreach (Module module in modulesList)
            {
                bool mainMenu = SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("MainMenu"));
                if (module.getCategory().Equals(Category.MainMenu) && !mainMenu
                    || !module.getCategory().Equals(Category.MainMenu) && mainMenu)
                {
                    continue;
                }
                if (module.enabled.Value)
                {
                    module.OnPreRender();
                }
            }
        }
        
        public void OnKeyPressed()
        {
            foreach (Module module in modulesList)
            {
                if (Input.GetKeyDown(module.bind.Bind))
                {
                    Toggle(module);
                }
            }
        }

        public bool IsModuleActive(Module module)
        {
            foreach (Module mod in modulesList)
            {
                if (mod.name == module.name && mod.category.Equals(module.category))
                {
                    return module.enabled.Value;
                }
            }

            return false;
        }

        public Module GetModule(string name)//I wanna get it by Class and not name :'(  
        {
            foreach (Module module in modulesList)
            {
                if (module.name.ToLower() == name.ToLower())
                {
                    return module;
                }
            }

            return null;
        }

        public void Toggle(Module module)
        {
            module.enabled.Value = !module.enabled.Value;
            if (module.enabled.Value)
            {
                Main.FileManager.Log(module.getName()+" Enabled!");
                module.OnEnable();
            }
            else
            {
                Main.FileManager.Log(module.getName()+" Disabled!");
                module.OnDisable();
            }
        }

        public void ToggleSettings(Module module)
        {
            module.showingSettings = !module.showingSettings;
        }
        
        public enum Category
        {
            Player,
            Movement,
            Server,
            Other,
            Hidden,
            MainMenu
        }
    }
}