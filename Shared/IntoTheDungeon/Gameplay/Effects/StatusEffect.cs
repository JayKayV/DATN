namespace IntoTheDungeon.Gameplay.Effect
{
    public enum EffectType
    {
        NONE = 0,
        BLOCK,
        BUFF,
        DEBUFF,
        OTHER
    }
    public class StatusEffect
    {
        public EffectType Type { get; set; } = EffectType.NONE;
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } = 0;
        public int Delay { get; set; } = 0;
        public bool Stackable { get; set; } = false;
        public StatusEffect(EffectType type, string name, string description, int duration, int delay)
        {
            Type = type;
            Name = name;
            Description = description;
            Duration = duration;
            Delay = delay;
            Stackable = false;
        }

        public StatusEffect(EffectType type, string name, string description, int duration, int delay, bool stackable)
        {
            Type = type;
            Name = name;
            Description = description;
            Duration = duration;
            Delay = delay;
            Stackable = stackable;
        }
    }   
}