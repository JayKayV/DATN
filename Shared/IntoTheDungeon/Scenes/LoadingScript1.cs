using IntoTheDungeon.Gameplay;
using Microsoft.Xna.Framework;
using SharedLibrary.Input;
using SharedLibrary.Scene;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using SharedLibrary.UIComponents;

namespace IntoTheDungeon.Scenes
{
    public class LoadingScript1 : SceneScript
    {
        private TextLabel loadingText;

        private CampaignMaster _campaignMaster;

        private Thread loadThread;

        private bool moveNext = false;
        private bool checkOnce = true;

        private void LoadCampaign()
        {
            _campaignMaster.Load(this.scene.GetContentManger(), this.scene.GetGraphicsDevice());
        }
        public override void Load()
        {
            loadingText = GameObjectManager.GetGameObjectByName("loadingText") as TextLabel;
            
            _campaignMaster = new CampaignMaster();
            _campaignMaster.Name = Constants.CAMPAIGN_MASTER;

            loadThread = new Thread(
                new ThreadStart(
                        LoadCampaign
                    )
                );
            
            loadThread.Start();
        }

        public override void Update(GameTime gameTime)
        {
            if (moveNext && KeyboardHandler.Instance.IsKeyDown(Keys.Space))
                LoadScene("Game Scene");

            if (!loadThread.IsAlive && checkOnce)
            {
                //Done loading
                //Debug.WriteLine("Loading is done!");

                ObjectStorage.AddObject(_campaignMaster.Name, _campaignMaster);
                loadingText.Text = "Loading is done! Please press space to continue";

                moveNext = true;
                checkOnce = false;
            }
        }
    }
}
