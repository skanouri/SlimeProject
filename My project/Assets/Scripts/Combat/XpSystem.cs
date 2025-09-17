using UnityEngine;

namespace SlimeRPG.Combat
{
    public class XpSystem : MonoBehaviour
    {
        public int level = 1;
        public int curXp = 0;
        public int xpToNext = 100;

        public System.Action onLevelUp;

        public void AddXp(int v)
        {
            curXp += v;
            while (curXp >= xpToNext)
            {
                curXp -= xpToNext;
                level++;
                xpToNext = 100 * level;
                // 레벨업 시 스탯 소폭 증가 (슬라임/플레이어만)
                var actor = GetComponent<Actor>();
                if (actor && actor.def.isPlayer)
                {
                    actor.cur.hp += 5;
                    actor.cur.atk += 2;
                }
                onLevelUp?.Invoke();
            }
        }
    }
}
