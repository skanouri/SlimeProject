using UnityEngine;

namespace SlimeRPG.Combat
{
    public class AutoAttacker : MonoBehaviour
    {
        public Actor self;
        public Actor target;
        float cd;

        private void Awake()
        {
            if (!self) self = GetComponent<Actor>();
        }

        private void Update()
        {
            if (!self || !target || self.IsDead || target.IsDead) return;
            cd -= Time.deltaTime;
            if (cd <= 0f)
            {
                target.TakeDamage(self.cur.atk);
                cd = 1f / Mathf.Max(0.1f, self.cur.atkSpeed);
            }
        }
    }
}
