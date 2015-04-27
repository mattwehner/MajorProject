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

        private void Start()
        {
            Debug.Log("WorldStorage Is Alive");

            Controller = new Controller();
        }

        private void Update()
        {
            Frame = Controller.Frame();
            Hand = Frame.Hands[0];
        }
    }
}