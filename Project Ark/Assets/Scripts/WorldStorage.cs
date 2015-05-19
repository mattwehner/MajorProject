using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class WorldStorage : MonoBehaviour
    {
        internal Controller Controller;
        internal Frame Frame;
        internal InteractionBox InteractionBox;
        internal Hand Hand;

        internal static bool IsPaused;
        internal static bool IsDebugOpen;
        internal bool KeyTapIsEnabled = false;
        internal string State;

        internal static Vector3 WayPointPosition { get; set; }
        internal static bool CompletedWayPoint = true;


        private void Start()
        {
            Debug.Log("WorldStorage Is Alive");

            Controller = new Controller();
        }

        private void Update()
        {
            if (KeyTapIsEnabled)
            {
                Controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP, KeyTapIsEnabled);
            }
            Frame = Controller.Frame();
            InteractionBox = Frame.InteractionBox;
            Hand = Frame.Hands[0];
        }
    }
}