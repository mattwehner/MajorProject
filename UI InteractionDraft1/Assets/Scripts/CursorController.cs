using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class CursorController : MonoBehaviour
    {
        public WorldManager WorldManager;

        void Update()
        {
            var frame = WorldManager.Controller.Frame();
            float pointerTouchDistance = frame.Hands[0].Fingers[0].TouchDistance;
            Ray collisionRay = new Ray(transform.position, Vector3.forward);
            RaycastHit hit;

            Debug.DrawRay(transform.position, Vector3.forward * 500);
            if (Physics.Raycast(collisionRay, out hit, 500))
            {
                if (hit.collider.tag == "SpawnButton")
                {
                    if (Input.GetMouseButtonDown(0) || hasClicked(frame))
                        WorldManager.SpawnSphere();
                }
            }
        }

        private bool hasClicked(Frame frame)
        {
            GestureList gesture = frame.Gestures();
            return (gesture[0].Type == Gesture.GestureType.TYPESCREENTAP) ? true : false;
        }
    }
}
