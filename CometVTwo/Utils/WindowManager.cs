using UnityEngine;

namespace CometVTwo.Utils
{
    /// <summary>
    /// Used to manage Unity's GUI windows IDs to prevent possible collision and glitches.
    /// </summary>
    public class WindowManager
    {
        private int windowCounter = 0;
        //Used to draw and increase counter.
        public Rect DrawWindow(Rect windowRect, GUI.WindowFunction function, string title)
        {
            windowCounter++;
            return GUI.Window(windowCounter, windowRect, function, title);
        }

        //Must be at the end to reset the counter.
        public void OnGUIEnd()
        {
            windowCounter = 0;
        }
    }
}