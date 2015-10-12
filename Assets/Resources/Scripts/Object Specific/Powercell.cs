using UnityEngine;
using System.Collections;
using Assets.Scripts.Object_Specific;

public class Powercell : MonoBehaviour, IPowered {
    public bool PoweredOn { get; set; }

    void Update () {
        if (PoweredOn)
        {
            transform.localRotation  = Quaternion.identity;
        }
	}

}
