using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;


namespace Assets.Scripts
{
    public class PublicReferenceList : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        public static PublicReferenceList publicReferenceList;

        public static GameObject Character;
        public GameObject Player;
        public static GameObject LeapController;
        public static GameObject Menu;
        public static GameObject DebugMenu;
        public GameObject CurrentMarker;
        public GameObject WayPointPrefab;

        void Awake()
        {
            Debug.Log("PublicReferenceList Is Alive");
            publicReferenceList = this;

            Character = GameObject.FindGameObjectWithTag("Arbie");
            Player = GameObject.FindGameObjectWithTag("Player");
            LeapController = GameObject.FindGameObjectWithTag("LeapController");
            Menu = GameObject.FindGameObjectWithTag("Menu");
            DebugMenu = GameObject.FindGameObjectWithTag("DebugMenu");

        }
    }

}
