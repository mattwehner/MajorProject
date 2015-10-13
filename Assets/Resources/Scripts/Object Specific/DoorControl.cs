using UnityEngine;

namespace Assets.Scripts.Object_Specific
{
    public class DoorControl : MonoBehaviour
    {
        public GameObject PoweredBy;
        private IPowerer _iPoweredObject;
        private Rigidbody _rigidbody;
        private Animator _animator;

        private bool _isPowered;
        private bool _isOpened;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            transform.localPosition = Vector3.zero;
            _isOpened = false;

            //_iPoweredObject = PoweredBy.GetComponent<IPowered>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
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
