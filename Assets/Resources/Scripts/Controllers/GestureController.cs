using System.Linq;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class GestureController : MonoBehaviour
    {
        private Frame _frame;
        private Hand _hand;
        public Camera Camera;
        public GameObject SetCursorMenu;
        public GameObject SelectCursor;
        private IMenuActioner _iMenuActioner;

        private Frame _storedFrame;

        private float _widthOffset;
        private float _heightOffset;

        void Awake()
        {
            _storedFrame = new Frame();
            SetCursorMenu.SetActive(false);
            _iMenuActioner = SetCursorMenu.GetComponent<IMenuActioner>();
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
                SelectCursor.transform.localPosition = CalculateCursorPosition();
                SetCursorMenu.SetActive(true);
                HandController handController = GameObject.FindGameObjectWithTag("GameController").GetComponent<HandController>();
                    handController.DestroyAllHands();
                handController.enabled = false;
                
                if (_iMenuActioner.SwitchCursor)
                {
                    UIController.Instance.CursorModeOn(true);
                    SetCursorMenu.SetActive(false);
                    _iMenuActioner.SwitchCursor = false;
                }
                return 0;
            }

            return HasSelectedCheck() ? 1 : 0;
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

        private Vector2 CalculateCursorPosition()
        {
            var interactionBox = _frame.InteractionBox;
            var handPosition = _frame.Hands[0].StabilizedPalmPosition;
            Vector leapHandPosition = interactionBox.NormalizePoint(handPosition);
            var x = (float)(211*(leapHandPosition.x -0.5))*2.5f;
            var y = (float)(211* (leapHandPosition.y - 0.5)+0.2)*2.5f;

            return new Vector2(x,y);
        }
    }
}
