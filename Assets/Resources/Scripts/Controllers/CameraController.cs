using System;
using Assets.Resources.Scripts.Controllers;
using Assets.Scripts;
using Leap;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Frame _frame;
    public GameObject Player;
    public GameObject Controller;
    public GameObject CameraIcon;
    public Camera Camera;

    private bool _cameraChecked;
    private Vector2 _cameraStart;
    private Vector4 cameraBounds;

    void Awake()
    {
        if (CameraIcon == null)
        {
            CameraIcon = GameObject.Find("Camera Icon");
        }
        CameraIcon.SetActive(false);
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

        cameraBounds = new Vector4(-13f,20f,-11f,2f);
    }

    private void Update()
    {

        _frame = HandMotionController.Instance.Controller.Frame();
        if (_frame.Hands.IsEmpty)
        {
            return;
        }

        FingerList fingers = _frame.Hands[0].Fingers;
        FingerList extendedFingers = fingers.Extended();
        FingerList thumb = fingers.FingerType(Finger.FingerType.TYPE_THUMB);
        FingerList pinkyFinger = fingers.FingerType(Finger.FingerType.TYPE_PINKY);

        bool isCamera = extendedFingers.Count == 2 &&
            extendedFingers[0].Equals(thumb[0]) &&
                extendedFingers[1].Equals(pinkyFinger[0]);

        if (isCamera)
        {
            CameraMovement();
        }
        else
        {
            _cameraChecked = false;
            CameraIcon.SetActive(false);
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
        InteractionBox interactionBox = _frame.InteractionBox;
        Vector handPosition = interactionBox.NormalizePoint(_frame.Hands[0].StabilizedPalmPosition);

        if (!_cameraChecked)
        {
            _cameraStart = new Vector2(handPosition.x, handPosition.y);
            CameraIcon.SetActive(true);
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