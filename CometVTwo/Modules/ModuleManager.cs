using System.Collections.Generic;
using CometVTwo.Modules.Hacks.InGame.Server;
using CometVTwo.Modules.Hacks.MainMenu;
using CometVTwo.Modules.Hacks.Movement;
using CometVTwo.Modules.Hacks.Other;
using CometVTwo.Modules.Hacks.Player;
using UnityEngine;

namespace CometVTwo.Modules
{
    public class ModuleManager
    {
        public List<Module> modulesList = new List<Module>();

        public void Init()
        {
            //InGame
            //Other
            modulesList.Add(new ClickMenu());
            modulesList.Add(new Aimbot());
            //Player
            modulesList.Add(new GiveAll());
            modulesList.Add(new GodMode());
            modulesList.Add(new RapidFire());
            modulesList.Add(new UnlimitedAmmo());
            modulesList.Add(new Projectile());
            //Movement
            modulesList.Add(new JumpModifier());
            modulesList.Add(new UseAmphetamineSalts());
            modulesList.Add(new NoClip());
            //Server
            modulesList.Add(new MapChanger());
            modulesList.Add(new ClientEditor());
            //Hidden
            //MainMenu
            modulesList.Add(new ClickMenuMainMenu());
            modulesList.Add(new ServerPassword());
            modulesList.Add(new ForceJoin());
            modulesList.Add(new CreateServer());
        }

        public void OnUpdate()
        {
            foreach (Module module in modulesList)
            {
                if (module.getCategory().Equals(Category.MainMenu) && Application.loadedLevelName != "MainMenu" 
                    || !module.getCategory().Equals(Category.MainMenu) && Application.loadedLevelName == "MainMenu")
                {
                    continue;
                }
                if (module.enabled)
                {
                    module.OnUpdate();
                }
            }
        }

        public void OnGUI() {
            foreach (Module module in modulesList)
            {
                if (module.getCategory().Equals(Category.MainMenu) && Application.loadedLevelName != "MainMenu" 
                    || !module.getCategory().Equals(Category.MainMenu) && Application.loadedLevelName == "MainMenu")
                {
                    continue;
                }
                if (module.enabled)
                {
                    module.OnGUI();
                }
            }
        }

        public void OnKeyPressed()
        {
            foreach (Module module in modulesList)
            {
                if (Input.GetKeyDown(module.bind.GetVelue()))
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
                    return module.enabled;
                }
            }

            return false;
        }

        public Module GetModule(string name)//I wanna get i by Class and not name :'(  
        {
            foreach (Module module in modulesList)
            {
                if (module.name == name)
                {
                    return module;
                }
            }

            return null;
        }

        public void Toggle(Module module)
        {
            Main.FileManager.Log(module.getName()+" Toggled!");
            module.enabled = !module.enabled;
            if (module.enabled)
            {
                module.OnEnable();
            }
            else
            {
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