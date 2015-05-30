using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    internal class StateInstructioner : MonoBehaviour {
        private PublicReferenceList _publicReferenceList;
        private PlayerController _playerController;
        private ArbieMaster _arbieMaster;

        private bool _wasJustPaused;

        void Start () {
	        Debug.Log("StateInstructioner Is Alive");
            RenderSettings.ambientLight = Color.black;

            _publicReferenceList = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<PublicReferenceList>();
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            _arbieMaster = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<ArbieMaster>();


        }
	
        void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) WorldStorage.IsPaused = !WorldStorage.IsPaused;
            if (Input.GetKeyDown(KeyCode.Tab)) WorldStorage.IsDebugOpen = !WorldStorage.IsDebugOpen;

            if (WorldStorage.IsPaused)
            {
                Time.timeScale = 0;
                _wasJustPaused = true;
                PublicReferenceList.LeapController.SetActive(false);
                PublicReferenceList.Menu.SetActive(true);
            }
            if(!WorldStorage.IsPaused && _wasJustPaused){
                _wasJustPaused = false;
                Time.timeScale = 1;
                PublicReferenceList.LeapController.SetActive(true);
            }
            if (WorldStorage.IsDebugOpen)
            {
                PublicReferenceList.DebugMenu.SetActive(true);
            }

        }
        public void ClearWayPoint()
        {
            WaypointController.Delete();
            _arbieMaster.WayPointRequestCompleted();
        }
        public void UpdateWayPoint(Vector3 tapPosition)
        {
            WaypointController.Create(tapPosition);
            _arbieMaster.MoveCharacterToWayPoint(WorldStorage.WayPointPosition);
            Debug.Log("Set Way Point");
        }

        public void BounderyPlayerMovement(string direction)
        {
            _playerController.BoundryMovementMaster(direction);
        }

        public static void ArbieExclamation(string exclamation)
        {
            if (exclamation == "way point reached")
            {
                WorldStorage.CompletedWayPoint = true;
            }
        }
    }
}
