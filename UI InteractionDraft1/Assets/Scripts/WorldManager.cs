using Leap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class WorldManager : MonoBehaviour
    {
        public Controller Controller;
        private bool _isPaused = false;
        private string _handMode;
        private int _previousExtendedCount;
        private string _previousHandMode;
        private object _previousHandPositionX;
        private object _previousHandPositionY;
        public float CameraSpeed;
        public Text HandModeDisplay;
        public GameObject MenuCursor;
        public GameObject Menu;
        public GameObject InGameUI;
        public GameObject Player;
        public GameObject PlayerHands;
        public GameObject Character;
        public Button SpawnButton;

        private void Start()
        {
            Controller = new Controller();
            Controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
            Controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            _previousExtendedCount = 0;
            HandModeDisplay.text = "";
            CameraSpeed = 0.03f;
            Cursor.visible = false;
        }

        
        private void Update()
        {
            var frame = Controller.Frame();
            var interactionBox = frame.InteractionBox;
            var hand = frame.Hands[0];
            var playerPosition = Player.transform.position;

            setObjectVisibility();

            if (Input.GetKeyDown(KeyCode.Escape))ToggleMenu();

            if (_isPaused || _handMode == "pointing")
            {
                MenuController(interactionBox, hand);
            }

            if (frame.Hands.Count == 0)
            {
                HandModeDisplay.text = "Place your hand into the scene";
                return;
            }
            

            HandModeCalculator(hand);

            if (_previousHandMode != _handMode)
            {
                HandModeDisplay.text = "You are " + _handMode;
                Debug.Log("Hand Mode is " + _handMode);
                _previousHandMode = _handMode;
            }

            if (_handMode == "camera")
            {
                CameraController(interactionBox, hand);
                if (playerPosition.x < -14)
                {
                    playerPosition.x = -13.99f;
                }
                if (playerPosition.x > 14)
                {
                    playerPosition.x = 13.99f;
                }
                if (playerPosition.y < -2)
                {
                    playerPosition.y = -1.99f;
                }
                if (playerPosition.y > 2)
                {
                    playerPosition.y = 1.99f;
                }
            }
        }

        private void setObjectVisibility()
        {
            Menu.SetActive(_isPaused);
            InGameUI.SetActive(!_isPaused);
            PlayerHands.SetActive(!_isPaused);

            Time.timeScale = _isPaused ? 0 : 1;
        }

        private void HandModeCalculator(Hand hand)
        {
            var fingers = hand.Fingers;
            var extendedFingers = fingers.Extended();

            var indexFinger = fingers.FingerType(Finger.FingerType.TYPE_INDEX);
            var thumb = fingers.FingerType(Finger.FingerType.TYPE_THUMB);
            var pinkyFinger = fingers.FingerType(Finger.FingerType.TYPE_PINKY);

            var isPointing = extendedFingers.Count == 1 &&
                             extendedFingers[0].Equals(indexFinger[0]);

            var isCamera = extendedFingers.Count == 2 &&
                           extendedFingers[0].Equals(thumb[0]) &&
                           extendedFingers[1].Equals(pinkyFinger[0]);

            if (extendedFingers.Count != _previousExtendedCount)
            {
                Debug.Log("Extended Fingers Count: " + extendedFingers.Count);
                _previousExtendedCount = extendedFingers.Count;
            }
            if (isPointing)
            {
                _handMode = "pointing";
                _isPaused = true;
                return;
            }
            _isPaused = false;

            if (isCamera)
            {
                _handMode = "camera";
                return;
            }
            _handMode = "grabbing";
        }

        private void CameraController(InteractionBox interactionBox, Hand hand)
        {
            var currentPosition = Player.transform.position;
            var handPosition = interactionBox.NormalizePoint(hand.StabilizedPalmPosition);
            var moveXAmount = 0f;
            var moveYAmount = 0f;
            const float cameraDeadZoneMinX = 0.48f;
            const float cameraDeadZoneMaxX = 0.52f;
            const float cameraDeadZoneMinY = 0.68f;
            const float cameraDeadZoneMaxY = 0.78f;


            if (handPosition.x > cameraDeadZoneMinX && handPosition.x < cameraDeadZoneMaxX)
            {
                moveXAmount = 0;
            }
            if (handPosition.x < cameraDeadZoneMinX)
            {
                moveXAmount = (float) (handPosition.x - 0.5);
            }
            if (handPosition.x > cameraDeadZoneMaxX)
            {
                moveXAmount = (float) (handPosition.x - 0.5);
            }
            if (handPosition.y > cameraDeadZoneMinY && handPosition.y < cameraDeadZoneMaxY)
            {
                moveYAmount = 0;
            }
            if (handPosition.y < cameraDeadZoneMinY)
            {
                moveYAmount = (float) (handPosition.y - 0.5);
            }
            if (handPosition.y > cameraDeadZoneMaxY)
            {
                moveYAmount = (float) (handPosition.y - 0.5);
            }

            var newDestination = new Vector3(Mathf.Clamp((Player.transform.position.x + moveXAmount), -14f, 14f),
                Mathf.Clamp((Player.transform.position.y + moveYAmount), -2f, 2f), 0);

            Player.transform.position = Vector3.Lerp(currentPosition, newDestination, 1);
        }

        private void MenuController(InteractionBox interactionBox, Hand hand)
        {

            Pointable pointerFinger = hand.Fingers.Frontmost;

            var width = (UnityEngine.Screen.width*1.5f) - (UnityEngine.Screen.width/2);
            var height = (UnityEngine.Screen.height * 1.5f) - (UnityEngine.Screen.height / 2);

            Vector leapFingerPosition = interactionBox.NormalizePoint(pointerFinger.StabilizedTipPosition);
            Vector3 unityFingerPosition = new Vector3(((leapFingerPosition.x) * width), (leapFingerPosition.y * height), -10);
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10);
            
            MenuCursor.transform.position = (hand.ToString() == "Invalid Hand")? mousePosition:unityFingerPosition;
        }

        public void SpawnSphere()
        {
            var defaultColor = SpawnButton.image.color;
            SpawnButton.image.color = Color.blue;
            GameObject.Instantiate(Character, new Vector3(0, 2, 6), Quaternion.identity);
            SpawnButton.image.color = defaultColor;
        }

        private void ToggleMenu()
        {
            _isPaused = !_isPaused;
        }
    }
}