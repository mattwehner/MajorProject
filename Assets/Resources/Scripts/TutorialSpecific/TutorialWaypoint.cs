using Assets.Resources.Scripts.Controllers;
using UnityEngine;

namespace Assets.Resources.Scripts.TutorialSpecific
{
    public class TutorialWaypoint : MonoBehaviour {
        public static TutorialWaypoint Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void Create(Vector3 position)
        {
            print("waypoint created at: " + position);

            var tapCoords = FindMarkerHeight(position);

            if (TutorialController.Instance.CurrentWaypoint == null)
            {
                Instantiate(TutorialController.Instance.Waypoint, tapCoords, Quaternion.identity);
                TutorialController.Instance.CurrentWaypoint = GameObject.Find("Waypoint(Clone)");
            }
            else
            {
                TutorialController.Instance.CurrentWaypoint.transform.position = tapCoords;
            }
            ArbieController.Instance.SetWaypoint(position);
        }

        private Vector3 FindMarkerHeight(Vector3 startPosition)
        {
            var newCoords = startPosition;

            var rayStart = new Vector3(startPosition.x, (startPosition.y), startPosition.z);
            var collisionRay = new Ray(rayStart, Vector3.down);
            RaycastHit hit;
            ;

            if (Physics.Raycast(collisionRay, out hit))
            {
                newCoords.y = hit.point.y;
            }
            return newCoords;
        }

        public void Delete()
        {
            Destroy(TutorialController.Instance.CurrentWaypoint);
            if (TutorialController.Instance.CurrentPhase == 1)
            {
                Phases.PhaseOne.HasReachedWaypoint = true;
            }
        }
    }
}
