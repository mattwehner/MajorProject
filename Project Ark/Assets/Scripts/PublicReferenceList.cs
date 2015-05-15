using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PublicReferenceList : MonoBehaviour
    {
        public GameObject Character;
        public GameObject Player;
        public static GameObject LeapController;
        public static GameObject Menu;
        public static GameObject DebugMenu;
        public Button DebugApplyButton;
        public Button DebugRevertButton;
        public Button DebugRestoreButton;
        public GameObject Object;
        public GameObject CurrentMarker;
        public GameObject WayPointPrefab;

        void Start()
        {
            Debug.Log("PublicReferenceList Is Alive");
            LeapController = GameObject.FindGameObjectWithTag("LeapController");
            Menu = GameObject.FindGameObjectWithTag("Menu");
            DebugMenu = GameObject.FindGameObjectWithTag("DebugMenu");
        }
    }

}
