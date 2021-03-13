using CometVTwo.Settings;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.Movement
{
    public class NoClip : Module
    {
        //Vars
        private Vector3 playerPos;
        
        //Settings
        private readonly doubleSetting speed = new doubleSetting("Speed", 1, 100, 1, 2);

        public NoClip()
        {
            base.SetUp("NoClip", ModuleManager.Category.Movement, KeyCode.V);
            this.moduleSettings.Add(speed);
        }

        public override void OnEnable()
        {
            playerPos = ((MyControllerScript[])UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0].transform.position;
        }

        public override void OnUpdate()
        {
            MyControllerScript script = ((MyControllerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0];
            if (Input.GetKey(KeyCode.W)) //Forward
            {
                Vector3 forward = script.transform.forward;
                playerPos += new Vector3((forward.x / 10) * this.speed.GetValueFloat(), forward.y , (forward.z / 10) * this.speed.GetValueFloat());
            }
            if (Input.GetKey(KeyCode.S)) //backwards
            {   
                Vector3 forward = script.transform.forward;
                playerPos -= new Vector3((forward.x / 10) * this.speed.GetValueFloat(), forward.y , (forward.z / 10) * this.speed.GetValueFloat());
            }
            if (Input.GetKey(KeyCode.A)) //Left
            {
                Vector3 right = script.transform.right;
                playerPos -= new Vector3((right.x / 10) * this.speed.GetValueFloat(), right.y , (right.z / 10) * this.speed.GetValueFloat());
            }
            if (Input.GetKey(KeyCode.D)) //Right
            {
                Vector3 right = script.transform.right;
                playerPos += new Vector3((right.x / 10) * this.speed.GetValueFloat(), right.y , (right.z / 10) * this.speed.GetValueFloat());
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Vector3 up = script.transform.up;
                playerPos += new Vector3(up.x, (up.y / 10) * this.speed.GetValueFloat() , up.z);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 up = script.transform.up;
                playerPos -= new Vector3(up.x, (up.y / 10) * this.speed.GetValueFloat() , up.z);
            }
            ((MyControllerScript[])UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0].transform.position = playerPos;
        }
    }
}