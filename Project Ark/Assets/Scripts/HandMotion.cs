using UnityEngine;
using Leap;
using UnityEditorInternal;

namespace Assets.Scripts
{
    internal class HandMotion : MonoBehaviour
    {
        private WorldStorage _worldStorage;
        private StateInstructioner _stateInstructioner;

        private Frame _frame;
        private Hand _hand;
        private float _minHandHeight;

        private string _state;

        void Start ()
        {
            Debug.Log("HandMotion Is Alive");

            _worldStorage = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldStorage>();
            _stateInstructioner = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<StateInstructioner>();

            _minHandHeight = Settings.Game.MinHandHeight;
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
                WorldStorage.IsPaused = true;
            }

            _worldStorage.State = HandModeCalculator(_hand);

            if (_state == "pointing")
            {
                _worldStorage.KeyTapIsEnabled = true;
                if (GestureTap.HasGroundTapped(_frame))
                {
                   _stateInstructioner.UpdateWayPoint(GestureTap.GestureKeyTapCoords);
                }
            }
            else { _worldStorage.KeyTapIsEnabled = false; }

            IsAtBoundry(_frame);
        }

        private void IsAtBoundry(Frame frame)
        {
            var normalizedHandPosition = _frame.InteractionBox.NormalizePoint(frame.Hands[0].PalmPosition);
            if (normalizedHandPosition.x == 0)
            {
                _stateInstructioner.BounderyPlayerMovement("move left");
            }
            if (normalizedHandPosition.x == 1)
            {
                _stateInstructioner.BounderyPlayerMovement("move right");
            }
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
        internal static Vector3 GestureKeyTapCoords = new Vector3(1,2,3);

        internal static bool HasGroundTapped(Frame frame)
        {
            
            var gesture = frame.Gestures();
            var keyTapPosition = new KeyTapGesture(gesture[0]).Position;
            
            bool hasGroundTapped = (gesture[0].Type == Gesture.GestureType.TYPEKEYTAP); 

            if (hasGroundTapped)
            {
                GestureKeyTapCoords = ConvertLeapToWorld.Point(keyTapPosition);
            }
            
            return hasGroundTapped;
        }
    }

    internal static class ConvertLeapToWorld {
        internal static Vector3 Point(Vector original){

            Vector3 unityPosition = original.ToUnityScaled();
            Vector3 worldPosition = PublicReferenceList.LeapController.transform.TransformPoint(unityPosition);
            return worldPosition;
        }
    }
}
