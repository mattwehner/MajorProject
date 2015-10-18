using System;
using System.Collections;
using Assets.Resources.Scripts.Storage;
using Assets.Scripts;
using Leap;
using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    public class ArbieController : MonoBehaviour
    {
        public static ArbieController Instance;

        private GameObject _camera;
        private bool _canClearWaypoint;
        private Collider _collider;
        private Controller _controller;
        private Vector3 _destination;
        private bool _scared;
        private bool _hasBeenThrown;
        private GameObject _message;
        private NavMeshAgent _navAgent;
        private bool _onGround;
        private Rigidbody _rigidbody;
        private AudioSource _moveSound;
        private GrabbableObject _grabbableObject;
        private NavMeshPathStatus lastPathStatus;
        private float lastRemainingDistance;
        

        void Awake()
        {
            Instance = this;
            _navAgent = GetComponent<NavMeshAgent>();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _moveSound = GetComponent<AudioSource>();
            _grabbableObject = gameObject.AddComponent<GrabbableObject>();
            EnableNavAgent(false);
            _moveSound.pitch = 0;
        }

        void Update()
        {
                _moveSound.pitch = (_onGround && _navAgent.enabled) ? 2 : 0;

            if (_navAgent.enabled && _canClearWaypoint && _onGround && _navAgent.remainingDistance < 0.11f)
            {
                PlaySound(1);
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

        void FixedUpdate()
        {
            if (_grabbableObject.IsGrabbed() || !_onGround)
            {CheckHeight();
            }
        }

        public void PlayMessage(int index)
        {
            InstantiateMessage(index);
        }

        public void PlaySound(int index)
        {
            InstantiateSound(index);
        }

        private void OnTriggerEnter(Collider collider)
        {
            _onGround = (collider.tag == "Level" || collider.tag == "Lift");

            if (collider.name.Contains("bone"))
            {
                EnableNavAgent(false);
                _hasBeenThrown = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name.Contains("bone"))
            {
                _hasBeenThrown = true;
            }
        }

        private void OnCollisionStay(Collision collision)
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
                    PlayMessage(3);
                    if (!_navAgent.hasPath){ EnableNavAgent(false);}
                    return;
                }
                EnableNavAgent(true);
                _navAgent.destination = destination;
                print("destination set at: " + Time.timeSinceLevelLoad);
                StartCoroutine(CanClearWaypoint());
            }
        }

        private void CheckHeight()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            print("Arbie is " + hit.distance + " off the ground");

            if (hit.distance > 4 && !_scared)
            {
                PlayMessage(15);
                _scared = true;
            }
                _scared = !(hit.distance < 4);
        }

        private void EnableNavAgent(bool enable)
        {
            _navAgent.enabled = enable;
            _rigidbody.isKinematic = enable;
            _collider.isTrigger = enable;
        }

        private IEnumerator UprightArbie()
        {
            yield return new WaitForSeconds(0.5f);
            _navAgent.enabled = false;
            _hasBeenThrown = false;
        }

        private void InstantiateMessage(int number)
        {
            Destroy(_message);
            StopCoroutine(DisplayMessage());
            _message = Instantiate(UnityEngine.Resources.Load("Prefabs/Arbie/message" + number)) as GameObject;
            _message.transform.SetParent(gameObject.transform, true);
            _message.transform.localPosition = Vector3.up*1.75f;
            StartCoroutine(DisplayMessage());
        }

        private void InstantiateSound(int index)
        {
            Destroy(_message);
            StopCoroutine(DisplayMessage());
            _message = Instantiate(UnityEngine.Resources.Load("Prefabs/Arbie/message0")) as GameObject;
            _message.GetComponent<AudioSource>().clip = ObjectRefences.Instance.SoundBites[index];
            _message.transform.SetParent(gameObject.transform, true);
            StartCoroutine(DisplayMessage());
        }

        private IEnumerator DisplayMessage()
        {
            yield return new WaitForSeconds(3);
            Destroy(_message);
            _destination = Vector3.zero;
            WaypointController.Instance.Delete();
        }

        private IEnumerator CanClearWaypoint()
        {
            _canClearWaypoint = false;
            yield return new WaitForSeconds(0.1f);
            _canClearWaypoint = true;
        }
    }
}