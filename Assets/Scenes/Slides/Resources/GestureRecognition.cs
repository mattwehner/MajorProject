using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Assets.Scripts;
using Leap;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.Slides.Resources
{
    public class GestureRecognition : MonoBehaviour
    {
        public Text Gesture;

        private Controller _controller;
        private Frame _frame;
        private Hand _hand;
        private float _heightOffset;
        private IMenuActioner _iMenuActioner;
        private Frame _storedFrame;

        private void Awake()
        {
            _controller = new Controller();
            _storedFrame = new Frame();
            Gesture.text = "talking";
        }

        private void Update()
        {
            _frame = _controller.Frame();
            _hand = _frame.Hands[0];
            _storedFrame = (_frame.Id > (_storedFrame.Id + 10)) ? _frame : _storedFrame;

            if (ActivateCursorCheck())
            {
                Gesture.text = "opening the menu";
            }
            if (HasSelectedCheck())
            {
                Gesture.text = "selecting";
            }
            if (IsCamera())
            {
                Gesture.text = "using the camera";
            }
        }

        private bool ActivateCursorCheck()
        {
            var extendedFingers = _frame.Hands[0].Fingers.Extended();
            var pinky = _hand.Fingers.FingerType(Finger.FingerType.TYPE_PINKY)[0];
            var index = _hand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];

            var containsPinky = (extendedFingers[0].Id == pinky.Id || extendedFingers[1].Id == pinky.Id);
            var containsIndex = (extendedFingers[0].Id == index.Id || extendedFingers[1].Id == index.Id);
            return (extendedFingers.Count == 2
                    && containsIndex
                    && containsPinky
                );
        }

        private bool HasSelectedCheck()
        {
            var _extendedFingers = _storedFrame.Hands[0].Fingers.Extended();
            var _thumb = _storedFrame.Hands[0].Fingers.FingerType(Finger.FingerType.TYPE_THUMB)[0];
            var _index = _storedFrame.Hands[0].Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];

            var before = (_extendedFingers[0].Id == _index.Id || _extendedFingers[1].Id == _index.Id)
                         && (_extendedFingers[0].Id == _thumb.Id || _extendedFingers[1].Id == _thumb.Id)
                         && (_extendedFingers.Count == 2);

            var extendedFingers = _frame.Hands[0].Fingers.Extended();
            var index = _hand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];

            var now = (extendedFingers[0].Id == index.Id || extendedFingers[1].Id == index.Id)
                      && (extendedFingers.Count == 1);
            return (before && now);
        }

        private bool IsCamera()
        {
            var fingers = _frame.Hands[0].Fingers;
            var extendedFingers = fingers.Extended();
            var thumb = fingers.FingerType(Finger.FingerType.TYPE_THUMB);
            var pinkyFinger = fingers.FingerType(Finger.FingerType.TYPE_PINKY);

            return (extendedFingers.Count == 2 &&
                    extendedFingers[0].Equals(thumb[0]) &&
                    extendedFingers[1].Equals(pinkyFinger[0])
                    );
        }
    }
}
