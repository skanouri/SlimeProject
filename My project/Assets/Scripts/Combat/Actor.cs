using UnityEngine;
using SlimeRPG.Data;

namespace SlimeRPG.Combat
{
    public class Actor : MonoBehaviour
    {
        public MonsterDef def;
        [HideInInspector] public Stats cur; // runtime
        public bool IsDead => cur.hp <= 0;

        private SpriteRenderer _sr;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            cur = def.baseStats;
            // 외형 적용
            if (_sr != null)
            {
                if (def.sprite) _sr.sprite = def.sprite;
                if (def.material) _sr.material = def.material;
            }
        }

        public void TakeDamage(int raw)
        {
            int dmg = Mathf.Max(1, raw - cur.def);
            cur.hp -= dmg;
            if (cur.hp <= 0) Die();
            else HitFlash();
        }

        void HitFlash()
        {
            if (_sr)
            {
                var c = _sr.color;
                _sr.color = new Color(1f, 0.9f, 0.3f, 1f);
                Invoke(nameof(ResetColor), 0.06f);
            }
        }
        void ResetColor()
        {
            if (_sr) _sr.color = Color.white;
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}
