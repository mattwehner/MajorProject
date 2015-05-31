using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaypointController : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        public static WaypointController waypointController;
        private PublicReferenceList _publicReferenceList;

        void Awake()
        {
            waypointController = this;
            
        }
        void Start () {
            Debug.Log("WaypointController is Alive");
            _publicReferenceList = PublicReferenceList.publicReferenceList;
        }
        
        public void Create(Vector3 tapPosition)
        {
            WayPointCreator(tapPosition);
        }

        private void WayPointCreator(Vector3 tapPosition)
        {
            
            _publicReferenceList = PublicReferenceList.publicReferenceList;

            Vector3 tapCoords = FindMarkerHeight(tapPosition);

            if (_publicReferenceList.CurrentMarker == null)
            {
                Instantiate(_publicReferenceList.WayPointPrefab, tapCoords, Quaternion.identity);
                _publicReferenceList.CurrentMarker = GameObject.Find("Marker(Clone)");
            }
            else
            {
                _publicReferenceList.CurrentMarker.transform.position = tapCoords;
            }

            WorldStorage.worldStorage.WayPointPosition = tapCoords;
            WorldStorage.worldStorage.CompletedWayPoint = false;
        }

        public void Delete()
        {
            Vector3 emptyVector = new Vector3(0,0,0);
            Destroy(_publicReferenceList.CurrentMarker);
            WorldStorage.worldStorage.WayPointPosition = emptyVector;
        }

        private Vector3 FindMarkerHeight(Vector3 startPosition)
        {
            var newCoords = startPosition;

            var rayStart = new Vector3(startPosition.x, (startPosition.y), startPosition.z);
            Ray collisionRay = new Ray(rayStart, Vector3.down);
            RaycastHit hit;;

            if (Physics.Raycast(collisionRay, out hit))
            {
                newCoords.y = hit.point.y;
            }
            return newCoords;
        }
    }
}
