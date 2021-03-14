using System.Collections.Generic;
using CometVTwo.Modules.Hacks.InGame.Movement;
using CometVTwo.Modules.Hacks.InGame.Other;
using CometVTwo.Modules.Hacks.InGame.Player;
using CometVTwo.Modules.Hacks.InGame.Server;
using CometVTwo.Modules.Hacks.MainMenu;
using UnityEngine;

namespace CometVTwo.Modules
{
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
                if (module.getCategory().Equals(Category.MainMenu) && Application.loadedLevelName != "MainMenu" 
                    || !module.getCategory().Equals(Category.MainMenu) && Application.loadedLevelName == "MainMenu")
                {
                    continue;
                }
                if (module.enabled.Value)
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
                if (module.enabled.Value)
                {
                    module.OnGUI();
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
            Main.FileManager.Log(module.getName()+" Toggled!");
            module.enabled.Value = !module.enabled.Value;
            if (module.enabled.Value)
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