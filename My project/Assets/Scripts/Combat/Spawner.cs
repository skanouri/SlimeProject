using UnityEngine;
using SlimeRPG.Data;
using SlimeRPG.Combat;

namespace SlimeRPG.Core
{
    public class Spawner : MonoBehaviour
    {
        public MonsterDef[] pool;
        public Transform area; // 사각 범위 빈 오브젝트
        public int maxEnemies = 5;
        public float spawnInterval = 2f;

        float timer;

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer > 0f) return;
            timer = spawnInterval;

            // 변경된 방식: Actor 전체 검색 (비활성화 제외)
            int alive = Object.FindObjectsByType<Actor>(FindObjectsSortMode.None).Length - 1;
            if (alive >= maxEnemies) return;

            if (pool == null || pool.Length == 0) return;
            var def = pool[Random.Range(0, pool.Length)];

            Vector2 pos = RandomPointInArea();
            var go = new GameObject($"Enemy_{def.id}");
            var sr = go.AddComponent<SpriteRenderer>();
            sr.drawMode = SpriteDrawMode.Simple;
            sr.sortingOrder = 0;
            var actor = go.AddComponent<Actor>(); actor.def = def;

            // 적 생성 직후 Hook 및 공격 컴포넌트 추가
            go.AddComponent<OnDestroyHook>().actor = actor;
            var aa = go.AddComponent<AutoAttacker>();
            aa.self = actor;

            go.transform.position = pos;

            // 변경된 방식: 플레이어 찾기
            var player = Object.FindFirstObjectByType<PlayerMarker>();
            if (player)
            {
                aa.target = player.GetComponent<Actor>();

                var patk = player.GetComponent<AutoAttacker>();
                if (patk && patk.target == null)
                    patk.target = actor;
            }
        }

        Vector2 RandomPointInArea()
        {
            if (!area) return Vector2.zero;
            var size = area.localScale;
            float x = Random.Range(-size.x / 2f, size.x / 2f);
            float y = Random.Range(-size.y / 2f, size.y / 2f);
            return (Vector2)area.position + new Vector2(x, y);
        }
    }
}
