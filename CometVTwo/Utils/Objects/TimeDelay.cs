using UnityEngine;

namespace CometVTwo.Utils.Objects
{
    public class TimeDelay
    {
        private float time = Time.deltaTime;

        public void Reset()
        {
            time = Time.deltaTime;
        }
        
        public bool TimePassed(float delay)
        {
            if(time <= delay)
            {
                time += Time.deltaTime;
                return false;
            };
            return true;
        }
    }
}