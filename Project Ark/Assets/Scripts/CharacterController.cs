using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        private NavMeshAgent _arbie;
        private Vector3 _arbiePosition;
        void Start()
        {
            Debug.Log("CharacterController Is Alive");
            _arbie = PublicReferenceList.Character;
        }

        void Update()
        {
            _arbiePosition = transform.position;
            if (_arbiePosition == WorldStorage.CurrentWayPoint) StateInstructioner.RecieveCommand("At Way Point");
            Debug.Log("_arbiePosition" + _arbiePosition);
            Debug.Log("CurrentWayPoint" + WorldStorage.CurrentWayPoint);
        }

        public void MoveToPosition(Vector3 Destination)
        {
            _arbie.SetDestination(Destination);
        }
    }
}
