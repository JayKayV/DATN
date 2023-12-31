using IntoTheDungeon.Gameplay.Units;
using System.Drawing;

namespace IntoTheDungeon.Gameplay.Action
{
    public class ActionExecutioner
    {
        public LevelTable LevelTable { get; set; }
        public ActionExecutioner(LevelTable levelTable) {
            LevelTable = levelTable;
        }

        public void Execute(Unit unit, GameAction action, params Point[] locations) { 
            
        }
    }
}