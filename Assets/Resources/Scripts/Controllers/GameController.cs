using System;
using Assets.Resources.Scripts.Object_Specific.Slaves;
using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        internal GameObject CurrentWaypoint;
        public GameObject HiddenObjects;
        public Animator SmallDoors;
        public GameObject Waypoint;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject.GetComponent<GameController>());
            }
            Instance = this;
            Destroy(HiddenObjects);
            Waypoint = UnityEngine.Resources.Load("Prefabs/Waypoint") as GameObject;
            UnityEngine.Cursor.visible = (Application.platform == RuntimePlatform.WindowsEditor);
        }

        void Start()
        {
            SpawnArbie();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnArbie();
            }
        }

        public void SpawnArbie()
        {
            Destroy(GameObject.FindGameObjectWithTag("Arbie"));
            var Arbie = Instantiate(UnityEngine.Resources.Load("Prefabs/Arbie/Arbie")) as GameObject;
            Arbie.name = "Arbie";
            Arbie.transform.position = new Vector3(-5.021008f, -1.139717f, 4.79f);
            SmallDoors.enabled = true;
            SmallDoors.Play("Open");
            print(Time.timeSinceLevelLoad);
            StartCoroutine(
                Utilities.WaitFor(
                    3f,
                    new Action(delegate { MoveArbie(Arbie); })
                    )
                );
            
        }

        private void MoveArbie(GameObject arbie)
        {
            StartCoroutine(Utilities.MoveFromTo(arbie.transform, new Vector3(-5.021008f, -1.139717f, 4.79f), new Vector3(-4.958269f, -1.139717f, 2.407f), 1));
            SmallDoors.Play("Close");
            arbie.GetComponent<ArbieController>().PlayMessage(4);
        }
    }
}