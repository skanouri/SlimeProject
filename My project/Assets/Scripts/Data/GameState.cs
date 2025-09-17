using System.Collections.Generic;
using UnityEngine;
using SlimeRPG.Data;

namespace SlimeRPG.Core
{
    public class GameState : MonoBehaviour
    {
        public static GameState I;

        [Header("Runtime State")]
        public MonsterId currentId = MonsterId.Slime;
        public int coins = 0;

        public Dictionary<MonsterId, int> kills = new Dictionary<MonsterId, int>();

        private void Awake()
        {
            if (I != null) { Destroy(gameObject); return; }
            I = this; DontDestroyOnLoad(gameObject);
        }

        public void AddKill(MonsterId id, int exp, int coin, Combat.XpSystem xp)
        {
            if (!kills.ContainsKey(id)) kills[id] = 0;
            kills[id]++;
            coins += coin;
            xp.AddXp(exp);
        }

        public int GetKillCount(MonsterId id) => kills.TryGetValue(id, out var v) ? v : 0;
    }
}
