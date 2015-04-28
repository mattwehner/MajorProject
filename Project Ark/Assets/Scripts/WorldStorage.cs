using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class WorldStorage : MonoBehaviour
    {
        public Controller Controller;
        public Frame Frame;
        public Hand Hand;
        public bool IsPaused = false;
        public bool KeyTapIsEnabled = false;
        public Vector3 MarkerCoords;
        public string State;

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
                Debug.Log("Key Tap Is Enabled");
            }
            Frame = Controller.Frame();
            Hand = Frame.Hands[0];
        }
    }
}