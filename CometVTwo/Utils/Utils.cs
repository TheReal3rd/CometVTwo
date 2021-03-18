using System;
using UnityEngine;

namespace CometVTwo.Utils
{
    public class Utils//To make life easier
    {
        private static int frameCount = 0;
        private static float deltaTime = 0.0f;
        private static float fps = 0.0f;
        private static float updateRate = 4.0f;
        
        public static int[] GetRGB (Color color)
        {
            return new[] {(int) (color.r * 255.0f), (int) (color.g * 255.0f), (int) (color.b * 255.0f)};
        }
        public static Color RGBToColour(int[] rgb)
        {
            return new Color((float)rgb[0] / 255, (float)rgb[1] / 255, (float)rgb[2] / 255, 1.0f);
        }
        public static Color RGBToColour(int r, int g, int b)
        {
            return new Color((float)r / 255, (float)g / 255, (float)b / 255, 1.0f);
        }

        public static float GetFrameRate()//Src https://answers.unity.com/questions/64331/accurate-frames-per-second-count.html
        {
            frameCount++;
            deltaTime += Time.unscaledDeltaTime;
            if (deltaTime > 1.0/updateRate)
            {
                fps = frameCount / deltaTime;
                frameCount = 0;
                deltaTime -= 1.0f / updateRate;
            }

            return (float)Math.Round(fps, 1);
        } 
    }
}