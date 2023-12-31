namespace IntoTheDungeon.Gameplay
{
    public enum GameDifficulty
    {
        NORMAL, HARD
    }
    public static class Settings
    {
        public static bool AudioOn { get; set; } = true;

        public static GameDifficulty GameDifficulty { get; set; } = GameDifficulty.NORMAL;
    }
}