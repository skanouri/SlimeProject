using System;
namespace SlimeRPG.Data
{
    [Serializable]
    public struct Stats
    {
        public int hp;
        public int atk;
        public int def;
        public float atkSpeed; // attacks per second
    }
}
