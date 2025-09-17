using UnityEngine;
using SlimeRPG.Data;
using SlimeRPG.Combat;

namespace SlimeRPG.Core
{
    public class EvolutionSystem : MonoBehaviour
    {
        public EvolutionRule[] rules;
        public GameObject evolutionPopup;
        GameState gs => GameState.I;
        Actor playerActor => GameObject.FindWithTag("Player")?.GetComponent<Actor>();
        XpSystem xp => GameObject.FindWithTag("Player")?.GetComponent<XpSystem>();

        private void Start()
        {
            if (xp != null) xp.onLevelUp += TryOpenPopup;
        }

        public void TryOpenPopup()
        {
            // ���� �����ϴ� ù ��Ģ ã�� (MVP: �ϳ���)
            foreach (var r in rules)
            {
                if (r.from != gs.currentId) continue;
                if (xp.level < r.requireLevel) continue;
                if (gs.GetKillCount(r.requireKill) < r.requireCount) continue;
                if (evolutionPopup) evolutionPopup.SetActive(true);
                return;
            }
        }

        public void ApplyEvolve()
        {
            foreach (var r in rules)
            {   
                if (r.from != gs.currentId) continue;
                if (xp.level < r.requireLevel) continue;
                if (gs.GetKillCount(r.requireKill) < r.requireCount) continue;

                // to�� ��ü
                var db = Resources.LoadAll<MonsterDef>("MonsterDefs"); // ���� ��ȸ(����: Addressables ����)
                MonsterDef target = null;
                foreach (var d in db) if (d.id == r.to) { target = d; break; }
                if (target == null) { Debug.LogWarning("Target MonsterDef not found"); return; }

                playerActor.def = target;
                playerActor.cur = target.baseStats;
                var sr = playerActor.GetComponent<SpriteRenderer>();
                if (sr)
                {
                    if (target.sprite) sr.sprite = target.sprite;
                    if (target.material) sr.material = target.material;
                }
                gs.currentId = r.to;
                if (evolutionPopup == null)
                {
                    evolutionPopup = GameObject.Find("EvolutionPopup"); // �̸� �Ǵ� �±׷� ã��
                }
                if (evolutionPopup) evolutionPopup.SetActive(false);
                break;
            }
        }

        public void ClosePopup()
        {
            if (evolutionPopup == null)
            {
                evolutionPopup = GameObject.Find("EvolutionPopup"); // �̸� �Ǵ� �±׷� ã��
            }
            if (evolutionPopup) evolutionPopup.SetActive(false);
        }
    }
}
