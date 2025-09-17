using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SlimeRPG.Data;

namespace SlimeRPG.Core
{
    [Serializable]
    public class SaveData
    {
        public MonsterId currentId;
        public int coins;
        public int level, curXp, xpToNext;
        public List<MonsterId> killKeys = new();
        public List<int> killVals = new();
    }

    public static class SaveLoad
    {
        static string PathFile => System.IO.Path.Combine(Application.persistentDataPath, "save.json");

        public static void Save(PlayerMarker player)
        {
            var gs = GameState.I;
            var xp = player.GetComponent<Combat.XpSystem>();

            var sd = new SaveData
            {
                currentId = gs.currentId,
                coins = gs.coins,
                level = xp.level,
                curXp = xp.curXp,
                xpToNext = xp.xpToNext
            };
            foreach (var kv in gs.kills) { sd.killKeys.Add(kv.Key); sd.killVals.Add(kv.Value); }

            File.WriteAllText(PathFile, JsonUtility.ToJson(sd));
        }

        public static void Load(PlayerMarker player)
        {
            var gs = GameState.I;
            var path = PathFile;
            if (!File.Exists(path))
            {
                return;
            }
            /*else
            {
                File.Delete(path);
                Debug.Log("세이브 파일이 삭제되었습니다.");
                return;
            }*/

            var sd = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
            gs.currentId = sd.currentId;
            gs.coins = sd.coins;
            gs.kills.Clear();
            for (int i = 0; i < sd.killKeys.Count; i++) gs.kills[sd.killKeys[i]] = sd.killVals[i];

            var xp = player.GetComponent<Combat.XpSystem>();
            xp.level = sd.level; xp.curXp = sd.curXp; xp.xpToNext = sd.xpToNext;
        }
    }
}
