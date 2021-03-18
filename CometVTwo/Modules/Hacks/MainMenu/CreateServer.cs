using CometVTwo.Modules.Hacks.InGame.Server;
using CometVTwo.Settings;

namespace CometVTwo.Modules.Hacks.MainMenu
{
    public class CreateServer : Module
    {
        //Vars
        //Settings
        private readonly enumSetting mapSelection = new enumSetting("Map", "Rift", MapChanger.maps);
        private readonly doubleSetting maxPlayers = new doubleSetting("MaxPlayers", 1, 32, 1, 32);
        private readonly doubleSetting minPlayers = new doubleSetting("MinPlayers", -32, 0, 1, -32);
        private readonly doubleSetting numPlayers = new doubleSetting("NumPlayers", 1, 32, 1, 32);
        private readonly booleanSetting autoStart = new booleanSetting("AutoStart", false);
        
        public CreateServer()
        {
            base.SetUp("CreateServer", ModuleManager.Category.MainMenu);
            this.moduleSettings.Add(mapSelection);
            this.moduleSettings.Add(maxPlayers);
            this.moduleSettings.Add(minPlayers);
            this.moduleSettings.Add(numPlayers);
            this.moduleSettings.Add(autoStart);
        }

        public override void OnEnable()
        {
            MapSelectScript[] mapSelectScripts = (MapSelectScript[]) UnityEngine.Object.FindObjectsOfType(typeof(MapSelectScript));
            MapManager[] mapManager = (MapManager[]) UnityEngine.Object.FindObjectsOfType(typeof(MapManager));
            foreach (MapSelectScript map in mapSelectScripts)
            {
                map.players.minValue = minPlayers.GetValueInt();
                map.players.maxValue = maxPlayers.GetValueInt();
                map.players.value = numPlayers.GetValueInt();
                map.mm.mapname = mapSelection.Selected;
            }
            if (autoStart.Value)
            {
                mapManager[0].CreateGame();
            }
            this.enabled.Value = false;
        }
    }
}