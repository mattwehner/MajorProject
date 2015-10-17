using Assets.Resources.Scripts.Controllers;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    public static WaypointController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Create(Vector3 position)
    {
        print("waypoint created at: " + position);

        var tapCoords = FindMarkerHeight(position);

        if (GameController.Instance.CurrentWaypoint == null)
        {
            Instantiate(GameController.Instance.Waypoint, tapCoords, Quaternion.identity);
            GameController.Instance.CurrentWaypoint = GameObject.Find("Waypoint(Clone)");
        }
        else
        {
            GameController.Instance.CurrentWaypoint.transform.position = tapCoords;
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
        Destroy(GameController.Instance.CurrentWaypoint);
    }
}