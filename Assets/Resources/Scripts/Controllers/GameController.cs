using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        public GameObject Waypoint;

        internal GameObject CurrentWaypoint;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject.GetComponent<GameController>());
            }
            Instance = this;

            Waypoint = UnityEngine.Resources.Load("Prefabs/Waypoint") as GameObject;
            UnityEngine.Cursor.visible = (Application.platform == RuntimePlatform.WindowsEditor);
        }
    }
}
