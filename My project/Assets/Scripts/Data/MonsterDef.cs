using UnityEngine;

namespace SlimeRPG.Data
{
    [CreateAssetMenu(menuName = "SlimeRPG/MonsterDef")]
    public class MonsterDef : ScriptableObject
    {
        public MonsterId id;
        public Sprite sprite;
        public Material material;   // null�̸� �⺻ SpriteRenderer.color ���
        public Stats baseStats;
        public int expReward = 5;
        public int coinReward = 1;
        public bool isPlayer = false; // ������ ����
    }
}
