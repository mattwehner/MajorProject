using UnityEngine;
using Leap;

namespace Assets.Scripts
{
    internal class HandMotion : MonoBehaviour
    {
        private WorldStorage _worldStorage;

        private Frame _frame;
        private Hand _hand;
        private float _minHandHeight;

        private string _state;

        void Start ()
        {
            Debug.Log("HandMotion Is Alive");

            _worldStorage = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldStorage>();

            _minHandHeight = PublicReferenceList.MinHandHeight;
        }
	
        void Update () {
            _state = _worldStorage.State;
            _frame = _worldStorage.Frame;
            _hand = _worldStorage.Hand;

            var interactionBox = _frame.InteractionBox;
            var handPosition = interactionBox.NormalizePoint(_hand.StabilizedPalmPosition);
            
            if (!_hand.IsValid)
            {
                return;
            }

            if (handPosition.y < _minHandHeight)
            {
                _worldStorage.IsPaused = true;
            }

            _worldStorage.State = HandModeCalculator(_hand);

            if (_state == "pointing")
            {
                _worldStorage.KeyTapIsEnabled = true;
                if (GestureTap.HasGroundTapped(_frame))
                {
                   StateInstructioner.RequestWayPoint(GestureTap.GestureTapCoords);
                }
            }
            else { _worldStorage.KeyTapIsEnabled = false; }
        }

        private string HandModeCalculator(Hand hand)
        {
            var state = "grabbing";
            var fingers = hand.Fingers;
            var extendedFingers = fingers.Extended();
            var indexFinger = fingers.FingerType(Finger.FingerType.TYPE_INDEX);

            var isPointing = extendedFingers.Count == 1 &&
                             extendedFingers[0].Equals(indexFinger[0]);

            if (isPointing) state = "pointing";

            return state;
        }
    }

    internal class GestureTap
    {
        internal static Vector GestureTapCoords;

        internal static bool HasGroundTapped(Frame frame)
        {
            var gesture = frame.Gestures();
            KeyTapGesture keyTap = new KeyTapGesture(gesture[0]);

            GestureTapCoords = keyTap.Position;
            return (gesture[0].Type == Gesture.GestureType.TYPEKEYTAP);
        }
    }
}
