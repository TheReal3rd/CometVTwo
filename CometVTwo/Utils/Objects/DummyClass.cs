using System;
using UnityEngine;

namespace CometVTwo.Utils.Objects
{
    /// <summary>
    /// A class that does nothing.
    /// </summary>
    public class DummyClass : MonoBehaviour
    {
        public void OnDestroy()
        {
            throw new NotImplementedException();
        }
    }
}