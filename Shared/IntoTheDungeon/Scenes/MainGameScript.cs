using IntoTheDungeon.Gameplay;
using Microsoft.Xna.Framework;
using SharedLibrary.Scene;
using SharedLibrary.TileSet;

namespace IntoTheDungeon.Scenes
{
    public class MainGameScript : SceneScript
    {
        private CampaignMaster _campaignMaster;

        public override void Load()
        {
            _campaignMaster = ObjectStorage.GetObject(Constants.CAMPAIGN_MASTER) as CampaignMaster;
            _campaignMaster.AlignCenter(Constants.GAME_AREA);

            Register(_campaignMaster);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}