using System.Collections.Generic;
using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules
{
    public class Module
    {
        public string name;
        public ModuleManager.Category category;
        public bindSetting bind = new bindSetting("Bind", KeyCode.None, Setting.SettingType.Bind);
        public bool enabled = false;
        public bool showingSettings = false;
        public List<Setting> moduleSettings = new List<Setting>();

        public void SetUp(string name, ModuleManager.Category category, KeyCode bindSet)
        {
            this.name = name;
            this.category = category;
            bind.SetValue(bindSet);
            this.moduleSettings.Add(bind);
        }
        public void SetUp(string name, ModuleManager.Category category)
        {
            this.name = name;
            this.category = category;
            this.moduleSettings.Add(bind);
        }

        
        public virtual void OnUpdate()// Executes OnUpdate code. (every frame) 
        {
            
        }

        public virtual void OnGUI() // Executes OnGUI code.
        {
            
        }

        public virtual void OnDisable() // Executes code when disabled.
        {
            
        }

        public virtual void OnEnable() // Executes code when enabled.
        {
            
        }

        public string getName()
        {
            return name;
        }

        public ModuleManager.Category getCategory()
        {
            return category;
        }
    }
}