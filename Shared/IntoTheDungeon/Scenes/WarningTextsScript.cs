using SharedLibrary.Scene;
using SharedLibrary.UIComponents;
using Microsoft.Xna.Framework;

namespace IntoTheDungeon.Scenes
{
    public class WarningTextsScript : SceneScript
    {
        private TextButton hideWarningsButton;
        private TextArea warningsArea;
        public override void Load()
        {
            hideWarningsButton = GameObjectManager.GetGameObjectByName("hideWarning") as TextButton;
            warningsArea = GameObjectManager.GetGameObjectByName("warningText") as TextArea;

            hideWarningsButton.HoverStyle.TextColor = Color.Yellow;
            hideWarningsButton.OnClick += HideWarningsButton_OnClick;
        }

        private void HideWarningsButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            if (warningsArea.Visible)
            {
                warningsArea.Visible = false;
                hideWarningsButton.Text = "Show warnings";
            } else
            {
                warningsArea.Visible = true;
                hideWarningsButton.Text = "Hide warnings";
            }
        }
    }
}