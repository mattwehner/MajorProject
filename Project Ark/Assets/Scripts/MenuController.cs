using UnityEngine;
using Leap;
using Image = UnityEngine.UI.Image;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MenuController : MonoBehaviour
    {
        private WorldStorage _worldStorage;
        private Frame _frame;

        public Image _cursor;
        public Image _closeMenu;
        public Image _nextLevel;
        private Color _defaultColor;
        private Color _hoverColor;
        private Color _activeColor;

        void Start () {
            Debug.Log("MenuController Is Alive");
            _worldStorage = WorldStorage.worldStorage;
            _defaultColor = _closeMenu.color;
            _hoverColor = Color.green;
            _activeColor = Color.blue;
        }
	
        void Update ()
        {
            //Checks to see if it is active when game is not paused
            if (PublicReferenceList.Menu.activeSelf & !_worldStorage.IsPaused)
            {
                CloseMenu();
                return;
            }
            if (_worldStorage.IsPaused) { 
            _frame = _worldStorage.Frame;
            CursorControler(_frame);
            CursorActioner(_frame.Hands[0]);
            }
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
            //magic number based on image size. Needs refactoring
            var w = 135;
            var h = 80;
            var cursor = _cursor.transform.position;
            var cM = _closeMenu.transform.position;
            var nL = _nextLevel.transform.position;
            bool isGrabbing = (hand.GrabStrength > 0.7);

            if (IsHovering(cursor, cM, w, h))
            {
                _closeMenu.color = _hoverColor;
                if (isGrabbing || Input.GetMouseButtonUp(0))
                {
                    _closeMenu.color = _activeColor;
                    _worldStorage.IsPaused = false;
                    return;
                }
            }
            else
            {
                _closeMenu.color = _defaultColor;
            }
            if (IsHovering(cursor, nL, w, h))
            {
                _nextLevel.color = _hoverColor;
                if (isGrabbing || Input.GetMouseButtonUp(0))
                {
                    var level = (Application.loadedLevel == 0) ? 1 : 0;
                    _nextLevel.color = _activeColor;
                    _worldStorage.IsPaused = false;
                    Time.timeScale = 1;
                    Application.LoadLevel(level);
                }
            }
            else
            {
                _nextLevel.color = _defaultColor;
            }
        }

        private bool IsHovering(Vector3 cursor, Vector3 element, int width, int height)
        {
            return (
                cursor.x > (element.x - width) &&
                cursor.x < (element.x + width) &&
                cursor.y > (element.y - height) &&
                cursor.y < (element.y + height)); ;
        }
    }
}
