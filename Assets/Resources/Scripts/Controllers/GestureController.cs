using System.Linq;
using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class GestureController : MonoBehaviour
    {
        private Frame _frame;
        private Hand _hand;
        public GameObject AskMenu;
        public GameObject MenuOption;
        public Camera Camera;

       private Frame _storedFrame;

        void Awake()
        {
            _storedFrame = new Frame();
            AskMenu.SetActive(false);
        }

        void Update()
        {
            _frame = HandMotionController.Instance.Controller.Frame();
            _hand = _frame.Hands[0];
            _storedFrame = (_frame.Id > (_storedFrame.Id + 10))? _frame : _storedFrame;
        }

        public int Calculate()
        {
            if (_hand == null)
            {
                return 0;
            }

            if (ActivateCursorCheck())
            {
                SetCursorCheckPositions();
                return 0;
            }
            AskMenu.SetActive(false);

            if (HasSelectedCheck())
            {
                return 1;
            }

            return 0;
        }

        private void SetCursorCheckPositions()
        {
            AskMenu.SetActive(true);
            var pinky = _hand.Fingers.FingerType(Finger.FingerType.TYPE_PINKY)[0].TipPosition.ToUnityScaled();
            var index = _hand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0].TipPosition.ToUnityScaled();
            AskMenu.transform.position =
                Camera.WorldToScreenPoint(transform.TransformPoint(new Vector3(pinky.x, pinky.y)));
            MenuOption.transform.position =
                Camera.WorldToScreenPoint(transform.TransformPoint(new Vector3(index.x, index.y)));
        }

        private bool ActivateCursorCheck()
        {
            FingerList extendedFingers = _frame.Hands[0].Fingers.Extended();
            Finger pinky = _hand.Fingers.FingerType(Finger.FingerType.TYPE_PINKY)[0];
            Finger index = _hand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];

            bool containsPinky = (extendedFingers[0].Id == pinky.Id || extendedFingers[1].Id == pinky.Id);
            bool containsIndex = (extendedFingers[0].Id == index.Id || extendedFingers[1].Id == index.Id);
            return (extendedFingers.Count == 2
                && containsIndex
                && containsPinky
                );
        }

        private bool HasSelectedCheck()
        {
            FingerList _extendedFingers = _storedFrame.Hands[0].Fingers.Extended();
            Finger _thumb = _storedFrame.Hands[0].Fingers.FingerType(Finger.FingerType.TYPE_THUMB)[0];
            Finger _index = _storedFrame.Hands[0].Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];

            bool before = (_extendedFingers[0].Id == _index.Id || _extendedFingers[1].Id == _index.Id)
                && (_extendedFingers[0].Id == _thumb.Id || _extendedFingers[1].Id == _thumb.Id)
                && (_extendedFingers.Count == 2);

            FingerList extendedFingers = _frame.Hands[0].Fingers.Extended();
            Finger index = _hand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];

            bool now = (extendedFingers[0].Id == index.Id || extendedFingers[1].Id == index.Id)
                && (extendedFingers.Count == 1);

            return (before && now);
        }
    }
}
