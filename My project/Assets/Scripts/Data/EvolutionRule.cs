using UnityEngine;

namespace SlimeRPG.Data
{
    [CreateAssetMenu(menuName = "SlimeRPG/EvolutionRule")]
    public class EvolutionRule : ScriptableObject
    {
        public MonsterId from;
        public MonsterId to;
        public MonsterId requireKill;
        public int requireCount = 10;
        public int requireLevel = 3;
    }
}
