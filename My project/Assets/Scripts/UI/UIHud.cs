using UnityEngine;
using UnityEngine.UI;
using SlimeRPG.Combat;
using SlimeRPG.Core;
using TMPro;

namespace SlimeRPG.UI
{
    public class UIHud : MonoBehaviour
    {
        public TextMeshProUGUI hpText;
        public Slider hpBar;
        public TextMeshProUGUI xpText;
        public Slider xpBar;
        public TextMeshProUGUI coinText;

        Actor playerActor;
        XpSystem xp;
        Core.GameState gs;

        private void Start()
        {
            gs = Core.GameState.I;
            var pm = Object.FindAnyObjectByType< PlayerMarker>();
            if (pm)
            {
                playerActor = pm.GetComponent<Actor>();
                xp = pm.GetComponent<XpSystem>();
            }
        }

        private void Update()
        {
            if (playerActor)
            {
                hpText.text = $"HP {Mathf.Max(0, playerActor.cur.hp)}";
                if (hpBar)
                {
                    var maxHp = Mathf.Max(1, playerActor.def.baseStats.hp);
                    hpBar.value = Mathf.Clamp01(playerActor.cur.hp / (float)maxHp);
                }
            }
            if (xp)
            {
                xpText.text = $"LV {xp.level}  {xp.curXp}/{xp.xpToNext}";
                if (xpBar) xpBar.value = Mathf.Clamp01(xp.curXp / (float)xp.xpToNext);
            }
            if (gs)
            {
                coinText.text = $"Coin {gs.coins}";
            }
        }
    }
}
