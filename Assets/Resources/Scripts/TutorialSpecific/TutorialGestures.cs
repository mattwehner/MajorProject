using Assets.Scripts;
using Leap;
using UnityEngine;

namespace Assets.Resources.Scripts.TutorialSpecific
{
    public class TutorialGestures : MonoBehaviour
    {
        public static TutorialGestures Instance;
        public Controller Controller;
        public bool IsCamera;
        private TutorialCamera _tutorialCamera;

        void Awake()
        {
            Instance = this;
            Controller = new Controller();
            _tutorialCamera = transform.parent.GetComponent<TutorialCamera>();
        }

        void Update()
        {
            if (Controller.Frame().Hands.IsEmpty)
            {
                return;
            }

            if (!TutorialController.Instance.InCursorMode)
            {
                IsCamera = IsCameraGesture();
                _tutorialCamera.IsCamera = (TutorialController.Instance.CanUseCamera && IsCamera);
            }
        }

        private bool IsCameraGesture()
        {
            var frame = Controller.Frame();
            var fingers = frame.Hands[0].Fingers;
            var extendedFingers = fingers.Extended();
            var thumb = fingers.FingerType(Finger.FingerType.TYPE_THUMB);
            var pinkyFinger = fingers.FingerType(Finger.FingerType.TYPE_PINKY);

            return (extendedFingers.Count == 2
                    && extendedFingers[0].Equals(thumb[0])
                    && extendedFingers[1].Equals(pinkyFinger[0]));
        }
    }
}
