using System.Collections.Generic;

namespace IntoTheDungeon.Gameplay
{
    public enum CampaignDifficulty
    {
        None = 0, EASY, NORMAL, HARD
    }

    public class Campaign
    {
        List<Stage> stages;
        CampaignDifficulty difficulty;

        public Campaign()
        {
            stages = new List<Stage>();
            difficulty = CampaignDifficulty.EASY;
        }

        public void LoadMetadata()
        {

        }
    }
}