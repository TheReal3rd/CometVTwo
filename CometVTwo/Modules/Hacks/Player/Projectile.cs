using CometVTwo.Settings;
using CometVTwo.Utils;
using UnityEngine;

namespace CometVTwo.Modules.Hacks.Player
{
    public class Projectile : Module
    {
        //Vars
        private AttackScript attackScript;
        private SelectionScript selectionScript;
        
        //Settings
        private readonly enumSetting projectile = new enumSetting("ProjectileType", "BULLET",
            new[] {"BULLET", "ARROW", "RIVET", "MORTAR"});
        private readonly doubleSetting speed = new doubleSetting("Speed", 0, 10, 1, 2);
        private readonly doubleSetting
            shotNum = new doubleSetting("ShotNum", 1, 100, 1, 5);
        private readonly doubleSetting inaccuracy =
            new doubleSetting("InAccuracy", 0, 10, 1, 0);
        private readonly doubleSetting damage =
            new doubleSetting("BulletDamage", 1, 1000, 10, 0);
        private readonly doubleSetting upPower =
            new doubleSetting("BulletUpPower", 1, 1000, 10, 0);
        private readonly booleanSetting doricnoise =
            new booleanSetting("BulletDoricnoise", false);
        private readonly booleanSetting ignoreTracers =
            new booleanSetting("BulletIgnoreTracers", false);
        private readonly booleanSetting semiAuto =
            new booleanSetting("SemiAuto", false);

        public Projectile()
        {
            base.SetUp("Projectile",ModuleManager.Category.Player);
            moduleSettings.Add(projectile);
            moduleSettings.Add(speed);
            moduleSettings.Add(shotNum);
            moduleSettings.Add(inaccuracy);
            moduleSettings.Add(damage);
            moduleSettings.Add(upPower);
            moduleSettings.Add(doricnoise);
            moduleSettings.Add(ignoreTracers);
            moduleSettings.Add(semiAuto);
        }

        public override void OnUpdate()
        {
            attackScript = (AttackScript) GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript));
            selectionScript = (SelectionScript) GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
            if (selectionScript.selectedweapon == 1 && (!semiAuto.GetValue() && Input.GetKey(KeyCode.Mouse0)) || (semiAuto.GetValue() && Input.GetKeyDown(KeyCode.Mouse0)))
            {
                if (projectile.GetSelected() == "BULLET")
                {
                    attackScript.shootbullet(inaccuracy.GetValueFloat(),1000f,shotNum.GetValueInt(), damage.GetValueFloat(), 1,speed.GetValueFloat(), upPower.GetValueFloat(), doricnoise.GetValue(), ignoreTracers.GetValue(), 1);
                }
                else if(projectile.GetSelected() == "ARROW")
                {
                    attackScript.throwprojectile(0,speed.GetValueFloat(), shotNum.GetValueFloat(), inaccuracy.GetValueFloat());
                } 
                else if (projectile.GetSelected() == "RIVET")
                {
                    attackScript.throwprojectile(1,speed.GetValueFloat(), shotNum.GetValueFloat(), inaccuracy.GetValueFloat());
                }
                else
                {
                    attackScript.throwprojectile(2,speed.GetValueFloat(), shotNum.GetValueFloat(), inaccuracy.GetValueFloat());
                }
            }
        }
    }
}