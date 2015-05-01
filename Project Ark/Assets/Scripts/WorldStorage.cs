using Leap;
using UnityEngine;
using Leap = Leap.Leap;

namespace Assets.Scripts
{
    public class WorldStorage : MonoBehaviour
    {
        public Controller Controller;
        public Frame Frame;
        public InteractionBox InteractionBox;
        public Hand Hand;
        public bool IsPaused;
        public bool KeyTapIsEnabled = false;
        public Vector3 MarkerCoords;
        public string State;
        public static Vector3 CurrentWayPoint { get; set; }

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