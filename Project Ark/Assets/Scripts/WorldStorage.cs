using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class WorldStorage : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        public static WorldStorage worldStorage;

        internal Controller Controller;
        internal Frame Frame;
        internal InteractionBox InteractionBox;
        internal Hand Hand;

        internal bool IsPaused = false;
        internal bool IsDebugOpen = false;
        internal bool KeyTapIsEnabled = false;
        internal string State;

        public int DebugValue = 100;

        internal Vector3 WayPointPosition { get; set; }
        internal bool CompletedWayPoint = true;

        void Awake()
        {
            //if (worldStorage == null)
            //{
            //    DontDestroyOnLoad(gameObject);
                worldStorage = this;
            //}
            //else if (worldStorage != this)
            //{
            //    Destroy(gameObject);
            //}
        }

        private void Start()
        {
            Debug.Log("WorldStorage Is Alive");
            Controller = new Controller();
        }

        private void Update()
        {
            Controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP, KeyTapIsEnabled);
            Controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE, IsPaused);

            Frame = Controller.Frame();
            InteractionBox = Frame.InteractionBox;
            Hand = Frame.Hands[0];
        }
    }
}