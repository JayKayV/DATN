namespace IntoTheDungeon.Gameplay
{ 
    public enum EnvironmentLight
    {
        DARK = 0, MOON_LIGHT = 1, LIGHT = 2, BRIGHT_LIGHT = 3
    }
    public class Stage
    {
        private string location;
        private string name;
        public bool Completed = false;
        private EnvironmentLight environmentLight;

        public Stage(string location, string name) {
            this.location = location;
            this.name = name;

            environmentLight = EnvironmentLight.DARK;
        }

        public EnvironmentLight Light { 
            get => environmentLight; 
            set => environmentLight = value;
        }
    }
}