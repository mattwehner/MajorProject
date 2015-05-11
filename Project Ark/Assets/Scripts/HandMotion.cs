using UnityEngine;
using Leap;

namespace Assets.Scripts
{
    internal class HandMotion : MonoBehaviour
    {
        private WorldStorage _worldStorage;
        private static PublicReferenceList _publicReferenceList;

        private Frame _frame;
        private Hand _hand;
        private float _minHandHeight;

        private string _state;

        void Start ()
        {
            Debug.Log("HandMotion Is Alive");

            _worldStorage = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldStorage>();
            _publicReferenceList = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<PublicReferenceList>();

            _minHandHeight = PublicReferenceList.MinHandHeight;
        }
	
        void Update () {
            _state = _worldStorage.State;
            _frame = _worldStorage.Frame;
            _hand = _worldStorage.Hand;

            var interactionBox = _frame.InteractionBox;
            var handPosition = interactionBox.NormalizePoint(_hand.StabilizedPalmPosition);
            var leapControllerPosition = _publicReferenceList.LeapController.transform.position;
            
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
                if (GestureTap.HasGroundTapped(_frame, interactionBox, leapControllerPosition))
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

            Frame frame = _worldStorage.Frame;
            InteractionBox interactionBox = frame.InteractionBox;
            Pointable pointerFinger = hand.Fingers.Frontmost;
            Debug.Log("Finger Position: " + interactionBox.NormalizePoint(pointerFinger.TipPosition));



            return state;
        }
    }

    internal class GestureTap
    {
        internal static Vector3 GestureTapCoords;

        internal static bool HasGroundTapped(Frame frame, InteractionBox interactionBox, Vector3 leapControllerPosition)
        {
            var gesture = frame.Gestures();
            Vector keyTapPosition = new KeyTapGesture(gesture[0]).Position;
            

            //GestureTapCoords = unityTapPosition;

            return (gesture[0].Type == Gesture.GestureType.TYPEKEYTAP);
        }
    }
}
