using CometVTwo.Settings;

namespace CometVTwo.Modules.Hacks.InGame.Server
{
    public class MapChanger : Module
    {
        private MultiplayerPlayerScript[] multiplayerPlayerScript;
        
        //Vars
        public static readonly string[] maps = new[]
        {
            "Fusillade", 
            "Vestibule", 
            "Skylab", 
            "Temple Mystica_RE", 
            "Clifftop", 
            "Holiday", 
            "Dusk Of The Dead", 
            "Erebus Reactor", 
            "Rift"
        };
        
        //Settings
        private readonly enumSetting mapSelection = new enumSetting("Maps", "Rift", maps);

        public MapChanger()
        {
            base.SetUp("MapChanger", ModuleManager.Category.Server);
            this.moduleSettings.Add(mapSelection);
        }

        public override void OnEnable()
        {
            multiplayerPlayerScript = (MultiplayerPlayerScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MultiplayerPlayerScript));
            multiplayerPlayerScript[0].changeMap(mapSelection.Selected);
            this.enabled.Value = false;
        }
    }
}