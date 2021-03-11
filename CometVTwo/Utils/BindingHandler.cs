using CometVTwo.Modules;
using UnityEngine;

namespace CometVTwo.Utils
{
    public class BindingHandler
    {
        private bool binding = false;
        private Module targetModule = null;

        public void UpdateBinding()
        {
            foreach (KeyCode keycode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keycode))
                {
                    targetModule.bind.SetValue(keycode);
                    binding = false;
                }
            }
        }

        public bool AreWeBinding()
        {
            return binding;
        }

        public void StartBinding(Module module)
        {
            if (!binding)
            {
                targetModule = module;
                binding = true;
            }
        }
    }
}