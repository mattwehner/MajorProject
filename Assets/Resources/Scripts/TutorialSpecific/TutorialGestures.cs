using Assets.Resources.Scripts.Controllers;
using Assets.Scripts;
using Assets.Scripts.Object_Specific;
using Leap;
using UnityEngine;

namespace Assets.Resources.Scripts.TutorialSpecific
{
    public class TutorialGestures : MonoBehaviour
    {
        public static TutorialGestures Instance;
        public Controller Controller;
        private TutorialCamera _tutorialCamera;

        public bool IsCamera;
        public bool IsPointing;

        internal bool InCollider;
        internal GameObject Triggering;

        private Frame _storedFrame;

        void Awake()
        {
            Instance = this;
            Controller = new Controller();
            _tutorialCamera = transform.parent.GetComponent<TutorialCamera>();
            _storedFrame = new Frame();
        }

        void Update()
        {
            _storedFrame = (Controller.Frame().Id > (_storedFrame.Id + 10)) ? Controller.Frame() : _storedFrame;

            if (Controller.Frame().Hands.IsEmpty)
            {
                return;
            }

            if (!TutorialController.Instance.InCursorMode)
            {
                IsCamera = IsCameraGesture();
                _tutorialCamera.IsCamera = (TutorialController.Instance.CanUseCamera && IsCamera);
                if (HasSelectedGesture())
                {
                    SelectInterpretor();
                }
            }
        }

        private void SelectInterpretor()
        {
            var inCollider = InCollider;
            var triggering = Triggering;

            if (!inCollider && triggering == null && TutorialController.Instance.CanSetWaypoint)
            {
                var fp = Controller.Frame().Hands[0]
                    .Fingers.Frontmost.TipPosition.ToUnityScaled();
                var transformed = transform.TransformPoint(fp);
                print("Setting Waypoint");
                TutorialWaypoint.Instance.Create(transformed);
            }

            //if (inCollider && Triggering != null)
            //{
            //    IInteractable interactable = Triggering.GetComponent<IInteractable>();
            //    interactable.Activate();
            //    if (!Triggering.name.Contains("Small") && Triggering.name != "Console_Arbie")
            //    {
            //        UIController.Instance.CursorModeOn(interactable.PoweredOn);
            //    }
            //}
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

        private bool HasSelectedGesture()
        {
            var _extendedFingers = _storedFrame.Hands[0].Fingers.Extended();
            var _thumb = _storedFrame.Hands[0].Fingers.FingerType(Finger.FingerType.TYPE_THUMB)[0];
            var _index = _storedFrame.Hands[0].Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];

            var before = (_extendedFingers[0].Id == _index.Id || _extendedFingers[1].Id == _index.Id)
                         && (_extendedFingers[0].Id == _thumb.Id || _extendedFingers[1].Id == _thumb.Id)
                         && (_extendedFingers.Count == 2);
            IsPointing = before;

            var extendedFingers = Controller.Frame().Hands[0].Fingers.Extended();
            var index = Controller.Frame().Hands[0].Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];

            var now = (extendedFingers[0].Id == index.Id || extendedFingers[1].Id == index.Id)
                      && (extendedFingers.Count == 1);
            return (before && now);
        }
    }
}
