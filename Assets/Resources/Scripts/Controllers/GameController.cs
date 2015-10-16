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
        }
    }
}
