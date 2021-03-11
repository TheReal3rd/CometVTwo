namespace CometVTwo.Settings
{
    public class enumSetting : Setting
    {
        //Not really a enum but a list of Strings that can be selected as modes.
        //Using enums will probably make it complex, Im saying probably as i IDK how enums work in C# fully & makes saving the data more easier.
        private string selected;
        private string[] Selection;
        private bool showingList = false;

        public enumSetting(string name, string description, string selected, string[] selection)
        {
            base.SetName(name);
            base.SetDescription(description);
            base.SetType(SettingType.Enum);
            this.selected = selected;
            this.Selection = selection;
        }
        public enumSetting(string name, string selected, string[] selection)
        {
            base.SetName(name);
            base.SetDescription("");
            base.SetType(SettingType.Enum);
            this.selected = selected;
            this.Selection = selection;
        }

        public string GetSelected()
        {
            return selected;
        }
        public void SetSelected(string selected)
        {
            this.selected = selected;
        }

        public string[] GetSelection()
        {
            return Selection;
        }

        public bool IsShowing()
        {
            return showingList;
        }
        public void ToggleShowing()
        {
            this.showingList = !showingList;
        }
    }
}