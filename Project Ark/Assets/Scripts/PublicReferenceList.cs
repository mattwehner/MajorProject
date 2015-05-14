using UnityEngine;

namespace Assets.Scripts
{
    public class PublicReferenceList : MonoBehaviour
    {
        public GameObject Character;
        public GameObject Player;
        public static GameObject LeapController;
        public GameObject Menu;
        public GameObject Object;
        public GameObject CurrentMarker;
        public GameObject WayPointPrefab;

        public static float MinHandHeight = 0.1f;

        void Start()
        {
            Debug.Log("PublicReferenceList Is Alive");
            LeapController = GameObject.FindGameObjectWithTag("LeapController");
        }
    }

}
