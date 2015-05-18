using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    internal class StateInstructioner : MonoBehaviour {
        private PublicReferenceList _publicReferenceList;
        private PlayerController _playerController;
        private CharacterMaster _characterMaster;

        void Start () {
	        Debug.Log("StateInstructioner Is Alive");
            RenderSettings.ambientLight = Color.black;

            _publicReferenceList = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<PublicReferenceList>();
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            _characterMaster = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<CharacterMaster>();
        }
	
        void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) WorldStorage.IsPaused = !WorldStorage.IsPaused;
            if (Input.GetKeyDown(KeyCode.Tab)) WorldStorage.IsDebugOpen = !WorldStorage.IsDebugOpen;

            if (WorldStorage.IsPaused)
            {
                PublicReferenceList.Menu.SetActive(true);
            }
            if (WorldStorage.IsDebugOpen)
            {
                PublicReferenceList.DebugMenu.SetActive(true);
            }
        }

        public void UpdateWayPoint(Vector3 tapPosition)
        {
            var wsCWP = WorldStorage.CurrentWayPoint;
            WorldStorage.CurrentWayPoint = WaypointController.WayPointMaster(tapPosition);

            _characterMaster.MoveCharacterToWayPoint(WorldStorage.CurrentWayPoint);

            Debug.Log("World Storage Way Point: " + WorldStorage.CurrentWayPoint);
        }

        public void BounderyPlayerMovement(string direction)
        {
            _playerController.BoundryMovementMaster(direction);
        }


        public static void RecieveCommand(string command)
        {
            if (command == "At Way Point")
            {
                Debug.Log("I am at the way point!");
            }
        }
    }
}
