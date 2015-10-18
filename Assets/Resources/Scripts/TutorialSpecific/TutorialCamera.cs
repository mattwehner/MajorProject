using Assets.Resources.Scripts.Controllers;
using Assets.Scripts;
using Leap;
using UnityEngine;

namespace Assets.Resources.Scripts.TutorialSpecific
{
    public class TutorialCamera : MonoBehaviour
    {
        public static TutorialCamera Instance;
        public bool IsCamera;

        private bool _cameraChecked;
        private Vector2 _cameraStart;
        private Frame _frame;
        public Camera Camera;
        public Vector4 CameraBounds;
        public GameObject Controller;
        public GameObject Player;

        private void Awake()
        {
            Instance = this;
            if (Player == null)
            {
                Player = GameObject.FindGameObjectWithTag("Player");
            }
            if (Controller == null)
            {
                Controller = GameObject.FindGameObjectWithTag("GameController");
            }
            if (Camera == null)
            {
                Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }

            CameraBounds = (CameraBounds == Vector4.zero) ? new Vector4(-20f, 20f, -11f, 2f) : CameraBounds;
        }

        private void Update()
        {
            _frame = TutorialGestures.Instance.Controller.Frame();
            if (_frame.Hands.IsEmpty)
            {
                return;
            }

            if (TutorialController.Instance.CanUseCamera)
            {
                if (IsCamera)
                {
                    CameraMovement();
                }
                else
                {
                    _cameraChecked = false;
                    if (!TutorialController.Instance.InCursorMode)
                    {
                        IsAtBoundry();
                    }
                }
            }

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, CameraBounds.x, CameraBounds.y),
                Mathf.Clamp(transform.position.y, CameraBounds.z, CameraBounds.w),
                0
                );
        }

        private void CameraMovement()
        {
            var interactionBox = _frame.InteractionBox;
            var handPosition = interactionBox.NormalizePoint(_frame.Hands[0].StabilizedPalmPosition);

            if (!_cameraChecked)
            {
                _cameraStart = new Vector2(handPosition.x, handPosition.y);
            }
            _cameraChecked = true;

            var newX = -(_cameraStart.x - handPosition.x);
            var newy = -(_cameraStart.y - handPosition.y);
            var movement = new Vector3(newX, newy, 0) * Settings.Player.CameraSensitivity;
            transform.Translate(movement);
        }

        private void IsAtBoundry()
        {
            var normalizedHandPosition = _frame.InteractionBox.NormalizePoint(_frame.Hands[0].StabilizedPalmPosition);
            var x = 0;
            var y = 0;
            if (normalizedHandPosition.x == 0)
            {
                x = (int)-Settings.Player.PlayerMovementSpeed;
            }
            if (normalizedHandPosition.x == 1)
            {
                x = (int)Settings.Player.PlayerMovementSpeed;
            }
            if (normalizedHandPosition.y <= 0.15)
            {
                y = (int)-Settings.Player.PlayerMovementSpeed;
            }
            if (normalizedHandPosition.y == 1)
            {
                y = (int)Settings.Player.PlayerMovementSpeed;
            }
            transform.Translate(new Vector3(x, y, 0) * Time.deltaTime);
        }
    }
}