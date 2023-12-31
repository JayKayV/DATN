using Microsoft.Xna.Framework;

namespace IntoTheDungeon.Gameplay
{
    public static class Constants
    {
        public static readonly Rectangle GAME_AREA = new Rectangle(200, 50, 880, 720);

        public static readonly string CAMPAIGN_MASTER = "CampaignMaster";

        public static readonly string INGAME_UPDATABLE = "ingame_updatable";

        //event names
        public static readonly string PLAYER_FINISH_CAMPAIGN = "player_finish";
        public static readonly string PLAYER_WIN = "player_win";
        public static readonly string PLAYER_LOSE = "player_lose";
        public static readonly string PLAYER_PAUSE = "player_pause";
        public static readonly string CHANGE_MAP = "player_change_map";

        public static readonly string TOGGLE_AUDIO = "toggle_audio";

        //special tile
        public static readonly string EXIT_TILE = "Exit";
    }
}