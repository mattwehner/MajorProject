using System.Collections.Generic;
using Assets.Resources.Scripts.Object_Specific;
using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        public GameObject Waypoint;
        public GameObject NavMeshBake;
        internal GameObject CurrentWaypoint;

        private Color32 _lightOn;
        private Color32 _lightOff;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject.GetComponent<GameController>());
            }
            Instance = this;
            NavMeshBake.SetActive(false);
            Waypoint = UnityEngine.Resources.Load("Prefabs/Waypoint") as GameObject;
            UnityEngine.Cursor.visible = (Application.platform == RuntimePlatform.WindowsEditor);

            _lightOn = new Color32(169,169,169,255);
            _lightOff = new Color32(30,30,30,255);
        }
    }
}
