using UnityEngine;

namespace Assets.Scripts
{
    internal class WaypointController : MonoBehaviour
    {
        private Vector3 _fingerTapPosition;
        // Use this for initialization
        void Start () {
            Debug.Log("WaypointController is Alive");
            Debug.Log("Gesture Tap Position" + GestureTap.GestureTapCoords);
        }
	
        // Update is called once per frame
        void Update ()
        {
        }
    }
}
