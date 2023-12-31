using System;
using System.Collections.Generic;

namespace IntoTheDungeon.Gameplay.Units
{
    public class UnitData
    {
        public string Name { get; set; }
        public int TileId {  get; set; }
        public int DeadTileId { get; set; }
        public int AttackRange { get; set; }
        public int MoveRange {  get; set; }
        public int EyeRange {  get; set; }
        public int Health {  get; set; }
        public int Armor {  get; set; }
        public int Attack {  get; set; }
        public int Block { get; set; }
        public bool IsLiving { get; set; } = false;
        public string Faction { get; set; } = "Neutral";

        public string Description { get; set; } = "Default description";
        public List<string> Items {  get; set; }
    }
}