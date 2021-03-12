using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CometVTwo.Utils
{
    public class PlayerUtil
    {
        //Entry points TODO remove this utils as the objects can get out of date then break the modules.
        public static PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
        public static SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
        public static AttackScript attackScript = (AttackScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript));
        public static MyControllerScript myControllerScript = (MyControllerScript) GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
        public static LadderUseScript ladderUseScript = (LadderUseScript) GameObject.Find("Player").GetComponent(typeof(LadderUseScript));
        public static PlayerPickupScript playerPickupScript = (PlayerPickupScript) GameObject.Find("Player").GetComponent(typeof(PlayerPickupScript));
        public static MultiplayerPlayerInformation multiplayerPlayerInformation = (MultiplayerPlayerInformation) GameObject.Find("Player").GetComponent(typeof(MultiplayerPlayerInformation));
        public static MyMouseLook myMouseLook = (MyMouseLook) GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
        //Render
        public static TextMeshProUGUI textMeshProUGUI = (TextMeshProUGUI) GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI));
        public static CrosshairSizeScript crosshairSizeScript = (CrosshairSizeScript) GameObject.Find("Crosshair").GetComponent(typeof (CrosshairSizeScript));
        public static CrosshairColorScript CrosshairColorScript = (CrosshairColorScript) GameObject.Find("Crosshair").GetComponent(typeof (CrosshairColorScript));
        public static Image image = (Image) GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image));
        

        

        
        //You can give yourself perks without providing a valid gameObject
        //((PlayerPickupScript)GameObject.Find("Player").GetComponent(typeof(PlayerPickupScript))).pickupholyhealth(new GameObject());
        
        

    }
}