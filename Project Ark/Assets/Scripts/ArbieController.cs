using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class ArbieController : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        internal static ArbieController arbieController;
        internal static NavMeshAgent ArbieNavMeshAgent;

        private ArbieMaster _arbieMaster;
        private NavMeshPath _currentPath;
        void Awake()
        {
            arbieController = this;

        }
        void Start()
        {
            Debug.Log("CharacterController Is Alive");
            _arbieMaster = ArbieMaster.arbieMaster;
            ArbieNavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        }

        void OnCollisionEnter(Collision collision)
        {
            _arbieMaster.OnCharacterCollision(collision);
        }

        internal void CreatePath(NavMeshPath path)
        {
            _currentPath = path;
            ArbieNavMeshAgent.SetPath(_currentPath);
        }

        internal void ClearPath()
        {
            ArbieNavMeshAgent.ResetPath();
        }
    }
}
