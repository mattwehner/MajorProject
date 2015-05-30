using UnityEngine;
using Leap;
using Image = UnityEngine.UI.Image;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MenuController : MonoBehaviour
    {
        private WorldStorage _worldStorage;
        private PublicReferenceList _publicReferenceList;
        private Frame _frame;
        private Image _cursor;
        private Image _button;
        private Color _defaultColor;
        private Color _hoverColor;
        private Color _activeColor;

        void Start () {
            Debug.Log("MenuController Is Alive");
            _worldStorage = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldStorage>();
            _publicReferenceList = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<PublicReferenceList>();
            _cursor = _publicReferenceList.Cursor;
            _button = _publicReferenceList.CloseMenuButton;
            _defaultColor = _button.color;
            _hoverColor = Color.green;
            _activeColor = Color.blue;
        }
	
        void Update () {
            //Checks to see if it is active when game is not paused
            if (PublicReferenceList.Menu.activeSelf & !WorldStorage.IsPaused) CloseMenu();

            _frame = _worldStorage.Frame;
            CursorControler(_frame);
            CursorActioner(_frame.Hands[0]);
        }

        public void CloseMenu()
        {
            Debug.Log("Close Menu");
            PublicReferenceList.Menu.SetActive(false);
        }

        private void CursorControler(Frame frame)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            var width = (UnityEngine.Screen.width * Settings.Game.HorizontalSensitivity) - (UnityEngine.Screen.width / 2);
            var height = (UnityEngine.Screen.height * Settings.Game.VerticalSensitivity) - (UnityEngine.Screen.height / 2);
            var interactionBox = frame.InteractionBox;
            var handPosition = frame.Hands[0].StabilizedPalmPosition;

            Vector leapHandPosition = interactionBox.NormalizePoint(handPosition);
            Vector3 cursorPosition = new Vector3(((leapHandPosition.x) * width), (leapHandPosition.y * height), -10);

            if (!frame.Hands[0].IsValid)
            {
                _cursor.transform.position = mousePosition;
                return;
            }
            
            _cursor.transform.position = cursorPosition;
        }

        private void CursorActioner(Hand hand)
        {
            var cursorPosition = _cursor.transform.position;
            var handPosition = hand.StabilizedPalmPosition;
            var w = 135;
            var h = 80;
            var bP = _button.transform.position;

            bool isHovering = (
                cursorPosition.x > (bP.x - w) &&
                cursorPosition.x < (bP.x + w) &&
                cursorPosition.y > (bP.y - h) &&
                cursorPosition.y < (bP.y + h));

            bool isGrabbing = (hand.GrabStrength > 0.7);

            if (isHovering && isGrabbing || isHovering && Input.GetMouseButtonUp(0))
            {
                _button.color = _activeColor;
                WorldStorage.IsPaused = false;
            }
            _button.color = (isHovering) ? _hoverColor : _defaultColor;
        }
    }
}
