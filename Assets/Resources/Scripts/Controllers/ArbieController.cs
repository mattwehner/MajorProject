using UnityEngine;

namespace Assets.Scripts
{
    public class ArbieController : MonoBehaviour
    {

        public static ArbieController Instance;

        private NavMeshAgent _navAgent;
        private Rigidbody _rigidbody;
        private bool _unreachedWaypoint;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject.GetComponent<ArbieController>());
            }

            Instance = this;
            _navAgent = GetComponent<NavMeshAgent>();
            _navAgent.enabled = false;
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = false;
            _unreachedWaypoint = false;
        }
        
        void Update()
        {
            if (_navAgent.enabled && _navAgent.remainingDistance < 0.11)
            {
                _navAgent.enabled = false;
                _rigidbody.isKinematic = false;
                WaypointController.Instance.Delete();
            }
        }

        internal void SetWaypoint(Vector3 destination)
        {
            _navAgent.enabled = true;
            _rigidbody.isKinematic = true;
            _navAgent.destination = destination;
            _unreachedWaypoint = true;
        }
    }
}
