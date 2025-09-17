using UnityEngine;

namespace SlimeRPG.Core
{
    public class GameController : MonoBehaviour
    {
        PlayerMarker player;
        void Start() { player = Object.FindAnyObjectByType<PlayerMarker>(); SaveLoad.Load(player); }
        void OnApplicationQuit() { if (player) SaveLoad.Save(player); }
    }
}
