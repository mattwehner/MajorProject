using System;
using System.Collections;
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

        private bool _hasBeenThrown;
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
        }
        
        void Update()
        {
            //if (_hasBeenThrown && (_rigidbody.velocity.magnitude <= Settings.Game.CharacterRecoverVelocity))
            //{
            //    StartCoroutine(WaitFor(5));
            //    _hasBeenThrown = false;
            //    _navAgent.enabled = true;
            //}

            throw new NotImplementedException();

            if (_navAgent.enabled && _navAgent.remainingDistance < 0.11)
            {
                _navAgent.enabled = false;
                _rigidbody.isKinematic = false;
                WaypointController.Instance.Delete();
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name.Contains("bone"))
            {
                _hasBeenThrown = true;
                _navAgent.enabled = false;
                _rigidbody.isKinematic = false;
            }
        }

        internal void SetWaypoint(Vector3 destination)
        {
            _navAgent.enabled = true;
            _rigidbody.isKinematic = true;
            _navAgent.destination = destination;
        }

        IEnumerator WaitFor(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }
    }
}
