using UnityEngine;
using SlimeRPG.Data;
using SlimeRPG.Core;

namespace SlimeRPG.Combat
{
    public class OnDestroyHook : MonoBehaviour
    {
        public Actor actor;
        void Awake() { if (!actor) actor = GetComponent<Actor>(); }
        void OnDestroy()
        {
            if (actor == null || actor.def == null) return;
            if (actor.def.isPlayer) return; // �÷��̾�� ����
            var gs = GameState.I;
            var xp = Object.FindAnyObjectByType<XpSystem>();// MVP�� ���� ����
            gs.AddKill(actor.def.id, actor.def.expReward, actor.def.coinReward, xp);
        }
    }
}
