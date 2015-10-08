using Assets.Resources.Scripts.Interfaces;
using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class ArbieController : MonoBehaviour, IGrabable
    {

        public static ArbieController Instance;
        public bool BeingGrabbed { get; set; }

        private NavMeshAgent _navAgent;
        private Rigidbody _rigidbody;
        private bool _unreachedWaypoint;

        private Controller _controller;

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

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.StartsWith("bone"))
            {
                var hand = collision.gameObject;
                if (_controller == null)
                {
                    _controller = HandMotionController.Instance.Controller;
                }
                BeingGrabbed = (_controller.Frame().Hands[0].GrabStrength > 0.5);
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
