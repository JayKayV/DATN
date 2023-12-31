using IntoTheDungeon.Gameplay;
using Microsoft.Xna.Framework.Media;
using SharedLibrary.Event;
using SharedLibrary.Scene;

namespace IntoTheDungeon.Scenes
{
    public class AudioScript : SceneScript
    {
        private Song menuSong;
        private Song ingameSong;

        private Song currentSong;

        public override void Load()
        {
            menuSong = scene.GetContentManager().Load<Song>("Audio/menu");
            ingameSong = scene.GetContentManager().Load<Song>("Audio/ingame");

            EventBus.Subscribe(Constants.TOGGLE_AUDIO, ToggleAudio);

            currentSong = menuSong;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(currentSong);

            this.scene.GetSceneManager().OnSceneLoaded += AudioScript_OnSceneLoaded;
        }

        private void AudioScript_OnSceneLoaded(object sender, OnSceneLoadedArgs e)
        {
            if (e.NewScene == "Game Menu")
                currentSong = ingameSong;
            else
                currentSong = menuSong;
        }

        private void ToggleAudio(Message message)
        {
            if (Settings.AudioOn)
            {
                MediaPlayer.Play(currentSong);
            }
            else
            {
                MediaPlayer.Stop();
            }
        }
    }
}