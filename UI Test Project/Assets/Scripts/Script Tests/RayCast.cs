using UnityEngine;
using System.Collections;

public class RayCast : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
        Ray collisionRay = new Ray(transform.position, Vector3.forward);
	    RaycastHit hit;

        Debug.DrawRay(transform.position, Vector3.forward * 500);
        if (Physics.Raycast(collisionRay, out hit, 500))
	    {
            if (hit.collider.tag == "SpawnButton")
	        {
	            Debug.Log("On Spawn Button");
	        }
	    }
	}
}
