using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaypointController : MonoBehaviour
    {
        private static PublicReferenceList _publicReferenceList;

        void Start () {
            Debug.Log("WaypointController is Alive");
        }

        private static Vector3 WayPointCreator(Vector3 tapPosition)
        {
            
            _publicReferenceList = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<PublicReferenceList>();

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
            return tapPosition;
        }

        public static Vector3 WayPointMaster(Vector3 tapPosition)
        {
            return WayPointCreator(tapPosition);
        }

        private static Vector3 FindMarkerHeight(Vector3 startPosition)
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
