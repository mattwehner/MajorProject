using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    internal class StateInstructioner : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        internal static StateInstructioner stateInstructioner;

        private WorldStorage _worldStorage;
        private PublicReferenceList _publicReferenceList;
        private PlayerController _playerController;
        private ArbieMaster _arbieMaster;
        private WaypointController _waypointController;

        private bool _wasJustPaused;

        void Awake()
        {
            stateInstructioner = this;
        }

        void Start () {
	        Debug.Log("StateInstructioner Is Alive");
            RenderSettings.ambientLight = Color.black;

            _worldStorage = WorldStorage.worldStorage;
            _publicReferenceList = PublicReferenceList.publicReferenceList;
            _playerController = PlayerController.playerController;
            _arbieMaster = ArbieMaster.arbieMaster;
            _waypointController = WaypointController.waypointController;
        }
	
        void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _worldStorage.IsPaused = !_worldStorage.IsPaused;
            if (Input.GetKeyDown(KeyCode.Tab)) _worldStorage.IsDebugOpen = !_worldStorage.IsDebugOpen;

            if (_worldStorage.IsPaused)
            {
                Time.timeScale = 0;
                _wasJustPaused = true;
                PublicReferenceList.LeapController.SetActive(false);
                PublicReferenceList.Menu.SetActive(true);
            }
            if(!_worldStorage.IsPaused && _wasJustPaused){
                _wasJustPaused = false;
                Time.timeScale = 1;
                PublicReferenceList.LeapController.SetActive(true);
            }
            if (_worldStorage.IsDebugOpen)
            {
                PublicReferenceList.DebugMenu.SetActive(true);
            }

        }
        public void ClearWayPoint()
        {
            if (!_publicReferenceList.CurrentMarker) return;
            _waypointController.Delete();
            _arbieMaster.WayPointRequestCompleted();
        }
        public void UpdateWayPoint(Vector3 tapPosition)
        {
            _waypointController.Create(tapPosition);
            _arbieMaster.MoveCharacterToWayPoint(_worldStorage.WayPointPosition);
            Debug.Log("Set Way Point");
        }

        public void BounderyPlayerMovement(string direction)
        {
            _playerController.BoundryMovementMaster(direction);
        }

        public  void ArbieExclamation(string exclamation)
        {
            if (exclamation == "way point reached")
            {
                _worldStorage.CompletedWayPoint = true;
            }
        }
    }
}
