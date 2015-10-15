using System;
using System.Collections;
using System.Runtime.InteropServices;
using Assets.Resources.Scripts.Interfaces;
using Assets.Scripts;
using JetBrains.Annotations;
using Leap;
using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    public class ArbieController : MonoBehaviour
    {

        public static ArbieController Instance;

        private Controller _controller;
        private NavMeshAgent _navAgent;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private GameObject _camera;
        private GameObject _message;

        private bool _onGround;
        private bool _hasBeenThrown;
        private Vector3 _destination;
        private bool _canClearWaypoint;

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

        private NavMeshPathStatus lastPathStatus;
        private float lastRemainingDistance;

        void Update()
        {
            if (_navAgent.enabled && _canClearWaypoint && _onGround && _navAgent.remainingDistance < 0.11f)
            {
                EnableNavAgent(false);
                WaypointController.Instance.Delete();
                _destination = Vector3.zero;
                print("destination cleared at: " + Time.timeSinceLevelLoad);
            }

            if (_onGround && _hasBeenThrown && _rigidbody.velocity.magnitude <= Settings.Game.CharacterRecoverVelocity)
            {
                if (_destination != Vector3.zero)
                {
                    EnableNavAgent(true);
                    _hasBeenThrown = false;
                    _navAgent.destination = _destination;
                }
                else
                {
                    _navAgent.enabled = true;
                    StartCoroutine(UprightArbie());
                }
               
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            _onGround = (collider.tag == "Level" || collider.tag == "Lift");

            if (collider.name.Contains("bone"))
            {
                EnableNavAgent(false);
                _hasBeenThrown = true;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name.Contains("bone"))
            {
                _hasBeenThrown = true;
            }
        }
        void OnCollisionStay(Collision collision)
        {
            _onGround = (collision.collider.tag == "Level" || collision.collider.tag == "Lift");
        }

        internal void SetWaypoint(Vector3 destination)
        {
            _destination = destination;
            var waypoint = GameController.Instance.CurrentWaypoint.transform.position.y;
            
            if (_onGround)
            {
                StopCoroutine(CanClearWaypoint());
                if (waypoint < transform.position.y - 1 || waypoint > transform.position.y + 1)
                {
                    InstantiateMessage(3);
                    return;
                }
                EnableNavAgent(true);
                _navAgent.destination = destination;
                print("destination set at: " + Time.timeSinceLevelLoad);
                StartCoroutine(CanClearWaypoint());
            }
        }

        private void EnableNavAgent(bool enable)
        {
            _navAgent.enabled = enable;
            _rigidbody.isKinematic = enable;
            _collider.isTrigger = enable;
        }

        IEnumerator UprightArbie()
        {
            
            yield return new WaitForSeconds(0.5f);
            _navAgent.enabled = false;
            _hasBeenThrown = false;
        }

        void InstantiateMessage(int number)
        {
            Destroy(_message);
            StopCoroutine(DisplayMessage());
            _message = Instantiate(UnityEngine.Resources.Load("Prefabs/Arbie/message" + number)) as GameObject;
            _message.transform.SetParent(gameObject.transform, true);
            _message.transform.localPosition = Vector3.up * 1.75f;
            _message.transform.Rotate(0,180,0,0);
            StartCoroutine(DisplayMessage());
        }

        IEnumerator DisplayMessage()
        {
            yield return new WaitForSeconds(3);
            Destroy(_message);
            _destination = Vector3.zero;
            WaypointController.Instance.Delete();
        }

        IEnumerator CanClearWaypoint()
        {
            _canClearWaypoint = false;
            yield return new WaitForSeconds(0.1f);
            _canClearWaypoint = true;
        }
    }
}
