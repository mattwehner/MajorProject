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

        private static Vector WayPointCreator(Vector tapPosition)
        {
            var tapCoords = new Vector3(tapPosition.x, tapPosition.y, tapPosition.z);
            _publicReferenceList = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<PublicReferenceList>();

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

        public static Vector WayPointMaster(Vector tapPosition)
        {
            return WayPointCreator(tapPosition);
        }
    }
}
