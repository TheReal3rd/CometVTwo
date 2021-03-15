using UnityEngine;

namespace CometVTwo
{
    /// <summary>
    /// For injection but not tested...
    /// </summary>
    public class Loader
    {
        private static GameObject _mainClass;

        public static void Load()
        {
            _mainClass = new GameObject();
            _mainClass.AddComponent<Main>();
            UnityEngine.Object.DontDestroyOnLoad(_mainClass);
        }
    }
}