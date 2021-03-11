using System.Collections.Generic;
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
            //Other
            //modulesList.Add(new TestModule2());
            //modulesList.Add(new SaveTest());
            //modulesList.Add(new LoadTest());
            //Player
            //modulesList.Add(new TestModule());
            modulesList.Add(new GiveAll());
            modulesList.Add(new GodMode());
            modulesList.Add(new RapidFire());
            modulesList.Add(new UnlimitedAmmo());
            //Movement
            modulesList.Add(new JumpModifier());
            //Server
        }

        public void OnUpdate() {
            for (int i =0; i != modulesList.Count; i++)
            {
                var mod = modulesList[i];
                if (mod.enabled)
                {
                    mod.OnUpdate();
                }
            }
        }

        public void OnGUI() {
            for (int i = 0; i != modulesList.Count; i++)
            {
                var mod = modulesList[i];
                if (mod.enabled)
                {
                    mod.OnGUI();
                }
            }
        }

        public void OnKeyPressed()
        {
            for (int i = 0; i != modulesList.Count; i++)
            {
                var mod = modulesList[i];
                if (Input.GetKeyDown(mod.bind.GetVelue()))
                {
                    Toggle(mod);
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

        public Module GetModule(string name)
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
            Other
        }
    }
}