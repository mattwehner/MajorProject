using UnityEngine;

namespace Assets.Scripts.Object_Specific
{
    public class DoorControl : MonoBehaviour
    {
        private Animator _animator;
        private IPowerer _iPoweredObject;
        private bool _isOpened;
        private bool _isPowered;
        private Rigidbody _rigidbody;
        public GameObject PoweredBy;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            transform.localPosition = Vector3.zero;
            _isOpened = false;

            //_iPoweredObject = PoweredBy.GetComponent<IPowered>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //_isPowered = _iPoweredObject.PoweredOn;

            if (_isPowered && !_isOpened)
            {
                OpenDoor();
            }
            if (!_isPowered && _isOpened)
            {
                CloseDoor();
            }
        }

        private void OpenDoor()
        {
            _animator.Play("asdf");
            _isOpened = true;
            _rigidbody.isKinematic = true;
        }

        private void CloseDoor()
        {
            _animator.Play("Door_Close");
            _isOpened = false;
            _rigidbody.isKinematic = false;
        }
    }
}