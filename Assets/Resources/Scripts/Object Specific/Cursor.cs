using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts.Interfaces;
using Assets.Scripts;
using Leap;
using UnityEngine.UI;

public class Cursor : MonoBehaviour, ICursor
{
    public GameObject Outer;
    public GameObject Inner;
    private RawImage _outerRawImage;
    private RawImage _innerRawImage;

    public float GrabStrength { get; set; }
    public bool IsGrabbing { get; set; }

    private float _widthOffset;
    private float _heightOffset;

    void Awake()
    {
        _widthOffset = (UnityEngine.Screen.width * Settings.Player.HorizontalSensitivity) - (UnityEngine.Screen.width / 2);
        _heightOffset = (UnityEngine.Screen.height * Settings.Player.VerticalSensitivity) - (UnityEngine.Screen.height / 2);

        _outerRawImage = Outer.GetComponent<RawImage>();
        _innerRawImage = Inner.GetComponent<RawImage>();
    }

    void FixedUpdate()
    {
        Controller controller = HandMotionController.Instance.Controller;
        GrabStrength = controller.Frame().Hands[0].GrabStrength;

        transform.position = CalculateCursorPosition(controller);

        var scaler = Mathf.Clamp((float)((-0.00549 * (GrabStrength * 100)) + 0.50549),0.15f, 0.5f);
        Outer.transform.localScale = new Vector3(scaler,scaler,scaler);
        IsGrabbing = (scaler < 0.17);

        if (IsGrabbing)
        {
            _outerRawImage.color = Color.green;
            _innerRawImage.color = Color.green;
        }
        if (!IsGrabbing && _innerRawImage.color == Color.green)
        {
            _outerRawImage.color = Color.white;
            _innerRawImage.color = Color.white;
        }
    }

    private Vector2 CalculateCursorPosition(Controller controller)
    {
        Vector2 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        var interactionBox = controller.Frame().InteractionBox;
        var handPosition = controller.Frame().Hands[0].StabilizedPalmPosition;
        Vector leapHandPosition = interactionBox.NormalizePoint(handPosition);

        float curserX = ((leapHandPosition.x)*_widthOffset);
        float restrictX = Mathf.Clamp(curserX, 0, UnityEngine.Screen.width);
        float curserY = (leapHandPosition.y * _heightOffset);
        float restrictY = Mathf.Clamp(curserY, 0, UnityEngine.Screen.height);
        Vector2 newCursorPosition = new Vector2(restrictX, restrictY);

        if (controller.Frame().Hands.IsEmpty)
        {
            GrabStrength = (Input.GetMouseButton(0)) ? 1 : 0;
        }

        return (controller.Frame().Hands.IsEmpty)?mousePosition: newCursorPosition;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (IsGrabbing)
        {
            IUiButton _iUiButton = collider.GetComponent<IUiButton>();
            if(_iUiButton != null)_iUiButton.ButtonAction();
        }
    }
}