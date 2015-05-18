using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterMaster : MonoBehaviour
    {
        private CharacterController _characterController;
        void Start () {
            Debug.Log("CharacterMaster Is Alive");
            _characterController = GameObject.FindGameObjectWithTag("Arbie").GetComponent<CharacterController>();
        }

        void Update()
        {
            
        }

        internal void MoveCharacterToWayPoint(Vector3 destination)
        {
           _characterController.MoveToPosition(destination);
        }
    }
}
