using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts
{
    public class Settings {
        void Start () {
            Debug.Log("GameSettings Is Alive");
        }
        internal class Player
        {
             internal static float PlayerMovementSpeed = 7f;
        }

        internal class Game
        {
            internal static float CharacterSpeed = 3f;
            internal static float MinHandHeight = 0.1f;
            internal static float CharacterRecoverVelocity = 0.1f;
            internal static float DistanceRemainingToWayPoint = 0.01f;
            internal static float SwipeQualifier = 0.3f;
        }
    }
}
