using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        private Vector3 _arbiePosition;
        void Start()
        {
            Debug.Log("CharacterController Is Alive");
        }

        void Update()
        {
            _arbiePosition = transform.position;
        }

        public void MoveToPosition(Vector3 Destination)
        {
            var current = _arbiePosition;
            var destination = Destination;

            transform.position = Vector3.Lerp(current, destination, 1);
        }
    }
}
