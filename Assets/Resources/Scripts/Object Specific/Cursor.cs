using Assets.Resources.Scripts.Interfaces;
using Assets.Scripts;
using Leap;
using UnityEngine;
using UnityEngine.UI;
using Screen = UnityEngine.Screen;

public class Cursor : MonoBehaviour, ICursor
{
    private float _heightOffset;
    private RawImage _innerRawImage;
    private RawImage _outerRawImage;
    private float _widthOffset;
    public GameObject Inner;
    public GameObject Outer;
    public float GrabStrength { get; set; }
    public bool IsGrabbing { get; set; }

    private void Awake()
    {
        _widthOffset = (Screen.width*Settings.Player.HorizontalSensitivity) - (Screen.width/2);
        _heightOffset = (Screen.height*Settings.Player.VerticalSensitivity) - (Screen.height/2);

        _outerRawImage = Outer.GetComponent<RawImage>();
        _innerRawImage = Inner.GetComponent<RawImage>();
    }

    private void FixedUpdate()
    {
        var controller = HandMotionController.Instance.Controller;
        GrabStrength = controller.Frame().Hands[0].GrabStrength;

        transform.position = CalculateCursorPosition(controller);

        var scaler = Mathf.Clamp((float) ((-0.00549*(GrabStrength*100)) + 0.50549), 0.15f, 0.5f);
        Outer.transform.localScale = new Vector3(scaler, scaler, scaler);
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
        var leapHandPosition = interactionBox.NormalizePoint(handPosition);

        var curserX = ((leapHandPosition.x)*_widthOffset);
        var restrictX = Mathf.Clamp(curserX, 0, Screen.width);
        var curserY = (leapHandPosition.y*_heightOffset);
        var restrictY = Mathf.Clamp(curserY, 0, Screen.height);
        var newCursorPosition = new Vector2(restrictX, restrictY);

        if (controller.Frame().Hands.IsEmpty)
        {
            GrabStrength = (Input.GetMouseButton(0)) ? 1 : 0;
        }

        return (controller.Frame().Hands.IsEmpty) ? mousePosition : newCursorPosition;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (IsGrabbing)
        {
            var _iUiButton = collider.GetComponent<IUiButton>();
            if (_iUiButton != null) _iUiButton.ButtonAction();
        }
    }
}