using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class ArbieController : MonoBehaviour
    {
        private NavMeshAgent _arbieNavMeshAgent;
        private ArbieMaster _arbieMaster;

        void Start()
        {
            Debug.Log("CharacterController Is Alive");
            _arbieMaster = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<ArbieMaster>();
            _arbieNavMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        }

        void OnCollisionEnter(Collision collision)
        {
            _arbieMaster.OnCharacterCollision(collision);
        }

        internal void CreatePath(NavMeshPath path)
        {
            _arbieNavMeshAgent.SetPath(path);
        }

        internal void ClearPath()
        {
            _arbieNavMeshAgent.ResetPath();
        }
    }
}
