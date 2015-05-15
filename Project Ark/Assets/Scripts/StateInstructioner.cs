using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    internal class StateInstructioner : MonoBehaviour {
        private WorldStorage _worldStorage;
        private PublicReferenceList _publicReferenceList;
        private PlayerController _playerController;

        void Start () {
	        Debug.Log("StateInstructioner Is Alive");

            _worldStorage = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldStorage>();
            _publicReferenceList = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<PublicReferenceList>();
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
	
        void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Pause) || Input.GetKeyDown(KeyCode.Space)) _worldStorage.IsPaused = !_worldStorage.IsPaused;
            if (Input.GetKeyDown(KeyCode.Tab))

            if (_worldStorage.IsPaused)
            {
                _publicReferenceList.Menu.SetActive(true);
            }
        }

        public void UpdateWayPoint(Vector3 tapPosition)
        {
            WorldStorage.CurrentWayPoint = WaypointController.WayPointMaster(tapPosition);
            Debug.Log("World Storage Way Point: " + WorldStorage.CurrentWayPoint);
        }

        public void BounderyPlayerMovement(string direction)
        {
            _playerController.BoundryMovementMaster(direction);
        }
    }
}
