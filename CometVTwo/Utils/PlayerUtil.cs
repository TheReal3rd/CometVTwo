using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CometVTwo.Utils
{
    public class PlayerUtil
    {
        //Entry points
        public static PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
        public static SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
        public static AttackScript attackScript = (AttackScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript));
        public static MyControllerScript myControllerScript = (MyControllerScript) GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
        public static Image image = (Image) GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image));
        public static LadderUseScript ladderUseScript = (LadderUseScript) GameObject.Find("Player").GetComponent(typeof(LadderUseScript));
        public static PlayerPickupScript playerPickupScript = (PlayerPickupScript) GameObject.Find("Player").GetComponent(typeof(PlayerPickupScript));
        //Render
        public static TextMeshProUGUI textMeshProUGUI = (TextMeshProUGUI) GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI));
        public static CrosshairSizeScript crosshairSizeScript = (CrosshairSizeScript) GameObject.Find("Crosshair").GetComponent(typeof (CrosshairSizeScript));
        public static CrosshairColorScript CrosshairColorScript = (CrosshairColorScript) GameObject.Find("Crosshair").GetComponent(typeof (CrosshairColorScript));
        
        public static void GiveAllWeapons()
        {
            if (selectionScript)
            {
                for (int i = 0; i < selectionScript.weaponinventory.Length; i++)
                {
                    selectionScript.weaponinventory[i] = true;
                }
                selectionScript.havedualpistols = true;
                selectionScript.permduals = true;
                selectionScript.permdaikatana = true;
                selectionScript.havedaikatana = true;
                selectionScript.permshotguns = true;
                selectionScript.havedualshotguns = true;
            }
        }
        
        public static void GiveKeys()
        {
            if (selectionScript)
            {
                selectionScript.haveredkey = true;
                selectionScript.havebluekey = true;
                selectionScript.haveyellowkey = true;
            }
        }

        public static void GiveMaxAmmo()
        {
            for (int i = 0; i != selectionScript.ammoinventory.Length; i++)
            {
                var maxAmmo = selectionScript.maxammo[i];
                selectionScript.ammoinventory[i] = maxAmmo;
            }
        }
        
        //You can give yourself perks without providing a valid gameObject
        //((PlayerPickupScript)GameObject.Find("Player").GetComponent(typeof(PlayerPickupScript))).pickupholyhealth(new GameObject());
        
        
        public static void SetPlayerHealth(float health)
        {
            playerHealthManagement.myhealth = health;
        }
        public static void HealthGodMode()
        {
            SetPlayerHealth(1000);
        }
        public static void BuiltInGodMode()
        {
            if (playerHealthManagement)
            {
                playerHealthManagement.godmode = !playerHealthManagement.godmode;
            }
        }
    }
}