using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class WorldStorage : MonoBehaviour
    {
        public static int AppWidth = UnityEngine.Screen.width;
        public static int AppHeight = UnityEngine.Screen.height;

        public Controller Controller;
        public Frame Frame;
        public InteractionBox InteractionBox;
        public Hand Hand;

        public bool IsPaused;
        public bool KeyTapIsEnabled = false;
        public Vector3 MarkerCoords;
        public string State;
        public static Vector3 CurrentWayPoint { get; set; }
        public float Offset = 371;

        //Settings
        public static float PlayerMovementSpeed = 7f;

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