using System;
using System.Collections;
using System.Runtime.InteropServices;
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
        private Collider _collider;

        private bool _isMoving;
        private bool _hasBeenThrown;
        private Controller _controller;

        private Vector3 _destination;


        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject.GetComponent<ArbieController>());
            }

            Instance = this;
            _navAgent = GetComponent<NavMeshAgent>();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            EnableNavAgent(false);
        }

        void OnColliderEnter(Collider collider)
        {
            
        }

        internal void SetWaypoint(Vector3 destination)
        {
            EnableNavAgent(true);
            _navAgent.destination = destination;
        }

        private void EnableNavAgent(bool enable)
        {
            _navAgent.enabled = enable;
            _rigidbody.isKinematic = enable;
            _collider.isTrigger = enable;
        }
    }
}
