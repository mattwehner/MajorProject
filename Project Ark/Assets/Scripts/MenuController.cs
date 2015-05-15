using UnityEngine;

namespace Assets.Scripts
{
    internal class MenuController : MonoBehaviour {
        void Start () {
            Debug.Log("MenuController Is Alive");
        }
	
        void Update () {
            //Checks to see if it is accidentaly active
            if (PublicReferenceList.Menu.activeSelf & !WorldStorage.IsPaused) PublicReferenceList.Menu.SetActive(false);
        }
    }
}
