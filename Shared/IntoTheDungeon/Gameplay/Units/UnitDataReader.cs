using SharedLibrary.Ultility;
using System.Collections.Generic;
using System.Diagnostics;

namespace IntoTheDungeon.Gameplay.Units
{
    public static class GameDataLoader
    {
        private const string UNITFILE_SRC = "gamedata/units.json";
        private const string GROUNDFILE_SRC = "gamedata/units.json";

        public static void Read()
        {
            List<UnitData> data = new List<UnitData>();
            data = JsonHelper.ReadJsonFile(UNITFILE_SRC, data.GetType()) as List<UnitData>;
        }
    }
}