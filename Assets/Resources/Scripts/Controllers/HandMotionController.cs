using Assets.Resources.Scripts.Controllers;
using Assets.Scripts.Object_Specific;
using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class HandMotionController : MonoBehaviour
    {
        public static HandMotionController Instance;
        internal bool CanCollide;
        private GestureController _gestureController;

        internal bool InCollider;
        internal GameObject Triggering;

        public Controller Controller;

        void Awake () {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject.GetComponent<HandMotionController>());
            }
            Instance = this;

            if (Controller == null)
            {
                Controller = new Controller();
            }
            _gestureController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GestureController>();
            CanCollide = false;
        }

        void Update()
        {
            if (Controller.Frame().Hands.IsEmpty)
            {
                return;
            }

            if (!UIController.Instance.IsActive)
            {
                switch (_gestureController.Calculate())
                {
                    case 0:
                        return;
                    case 1:
                        HasClicked();
                        break;
                }
            }
        }

        void FixedUpdate()
        {
            HasKeyboardOveride();
        }

        private void HasClicked()
        {
            var inCollider = InCollider;
            var triggering = Triggering;

            if (!inCollider && triggering == null)
            {
                var fp = Controller.Frame().Hands[0]
                    .Fingers.Frontmost.TipPosition.ToUnityScaled();
                var transformed = transform.TransformPoint(fp);
                print("Setting Waypoint");
                WaypointController.Instance.Create(transformed);
            }

            if (inCollider && Triggering != null)
            {
                IInteractable interactable = Triggering.GetComponent<IInteractable>();
                interactable.Activate();
                if (!Triggering.name.Contains("Small") && Triggering.name != "Console_Arbie")
                {
                    UIController.Instance.CursorModeOn(interactable.PoweredOn);
                }
            }
        }

        public void SetCollider(bool b, GameObject o)
        {
            InCollider = b;
            Triggering = o;
        }

        private void HasKeyboardOveride()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CanCollide = !CanCollide;
                var hc = GetComponent<global::HandController>();

                hc.IgnoreCollisionsWithHands(GameObject.FindWithTag("Arbie"), CanCollide);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIController.Instance.CursorModeOn(true);
            }
        }
    }
}