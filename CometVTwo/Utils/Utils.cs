using UnityEngine;

namespace CometVTwo.Utils
{
    public class Utils//To make life easier
    {
        public static int[] GetRGB (Color color)
        {
            return new[] {(int) (color.r * 255.0f), (int) (color.g * 255.0f), (int) (color.b * 255.0f)};
        }
        public static Color RGBToColour(int[] rgb)
        {
            return new Color((float)rgb[0] / 255, (float)rgb[1] / 255, (float)rgb[2] / 255, 1.0f);
        }
        
    }
}