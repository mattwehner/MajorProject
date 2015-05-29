using UnityEngine;

namespace Assets.Scripts
{
    public class MenuController : MonoBehaviour {
        void Start () {
            Debug.Log("MenuController Is Alive");
        }
	
        void Update () {
            //Checks to see if it is accidentaly active
            if (PublicReferenceList.Menu.activeSelf & !WorldStorage.IsPaused) PublicReferenceList.Menu.SetActive(false);
        }

        public void CloseMenu()
        {
            Debug.Log("Close Menu");
        }
    }
}
