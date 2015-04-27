using UnityEngine;

namespace Assets.Scripts
{
    internal class MenuController : MonoBehaviour {
        private WorldStorage _worldStorage;
        private PublicReferenceList _publicReferenceList;

        void Start () {
            Debug.Log("MenuController Is Alive");

            _worldStorage = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldStorage>();
            _publicReferenceList = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<PublicReferenceList>();
        }
	
        void Update () {
            if (_publicReferenceList.Menu.activeSelf & !_worldStorage.IsPaused) _publicReferenceList.Menu.SetActive(false);
        }
    }
}
