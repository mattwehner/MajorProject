using Assets.Resources.Scripts.Controllers;
using Assets.Scripts;
using Leap;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool _cameraChecked;
    private Vector2 _cameraStart;
    private Frame _frame;
    public Camera Camera;
    private Vector4 cameraBounds;
    public GameObject Controller;
    public GameObject Player;

    private void Awake()
    {
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

        cameraBounds = new Vector4(-20f, 20f, -11f, 2f);
    }

    private void Update()
    {
        _frame = HandMotionController.Instance.Controller.Frame();
        if (_frame.Hands.IsEmpty)
        {
            return;
        }

        var fingers = _frame.Hands[0].Fingers;
        var extendedFingers = fingers.Extended();
        var thumb = fingers.FingerType(Finger.FingerType.TYPE_THUMB);
        var pinkyFinger = fingers.FingerType(Finger.FingerType.TYPE_PINKY);

        var isCamera = extendedFingers.Count == 2 &&
                       extendedFingers[0].Equals(thumb[0]) &&
                       extendedFingers[1].Equals(pinkyFinger[0]);

        if (isCamera)
        {
            CameraMovement();
        }
        else
        {
            _cameraChecked = false;
            if (!UIController.Instance.IsActive)
            {
                IsAtBoundry();
            }
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraBounds.x, cameraBounds.y),
            Mathf.Clamp(transform.position.y, cameraBounds.z, cameraBounds.w),
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
        var movement = new Vector3(newX, newy, 0)*Settings.Player.CameraSensitivity;
        transform.Translate(movement);
    }

    private void IsAtBoundry()
    {
        var normalizedHandPosition = _frame.InteractionBox.NormalizePoint(_frame.Hands[0].StabilizedPalmPosition);
        var x = 0;
        var y = 0;
        if (normalizedHandPosition.x == 0)
        {
            x = (int) -Settings.Player.PlayerMovementSpeed;
        }
        if (normalizedHandPosition.x == 1)
        {
            x = (int) Settings.Player.PlayerMovementSpeed;
        }
        if (normalizedHandPosition.y <= 0.15)
        {
            y = (int) -Settings.Player.PlayerMovementSpeed;
        }
        if (normalizedHandPosition.y == 1)
        {
            y = (int) Settings.Player.PlayerMovementSpeed;
        }
        transform.Translate(new Vector3(x, y, 0)*Time.deltaTime);
    }
}