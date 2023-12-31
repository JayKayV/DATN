using SharedLibrary.Scene;
using System.Diagnostics;

namespace IntoTheDungeon.Scenes
{
    public class Example : SceneScript
    {
        public override void Load()
        {
            Say();
        }

        public void Say()
        {
            Debug.WriteLine("I am saying something", "HELLO");
        }
    }
}