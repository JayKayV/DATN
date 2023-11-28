namespace Shared.Gameplay.Grounds
{
    public class GroundData
    {
        public int tileId { get; set; }
        public string name { get; set; }
        public bool walkable { get; set; }
        public bool seethrough { get; set; }
        public string[] effects { get; set; }
    }
}
