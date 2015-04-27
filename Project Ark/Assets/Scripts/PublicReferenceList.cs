using UnityEngine;

namespace Assets.Scripts
{
    public class PublicReferenceList : MonoBehaviour
    {
        public GameObject Character;
        public GameObject Player;
        public GameObject LeapController;
        public GameObject Camera;
        public GameObject Menu;
        public GameObject Object;

        public static float MinHandHeight = 0.1f;

        void Start()
        {
            Debug.Log("PublicReferenceList Is Alive");
        }
    }
}
