using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    internal class StateInstructioner : MonoBehaviour {
        private WorldStorage _worldStorage;
        private PublicReferenceList _publicReferenceList;

        void Start () {
	        Debug.Log("StateInstructioner Is Alive");

            _worldStorage = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldStorage>();
            _publicReferenceList = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<PublicReferenceList>();
        }
	
        void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _worldStorage.IsPaused = !_worldStorage.IsPaused;

            if (_worldStorage.IsPaused)
            {
                _publicReferenceList.Menu.SetActive(true);
            }
        }

        public static void RequestWayPoint(Vector3 tapPosition)
        {
            WorldStorage.CurrentWayPoint = WaypointController.WayPointMaster(tapPosition);
            Debug.Log(WorldStorage.CurrentWayPoint);
        }
    }
}
