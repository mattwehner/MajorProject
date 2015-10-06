using UnityEngine;
using System.Collections;
using Assets.Scripts.Object_Specific;
using System;
using Assets.Scripts;

public class Terminal : MonoBehaviour
{
    public bool PoweredOn { get;set;}
    public GameObject InteractionBounds { get; set; }
    public bool IsActive { get; set; }

    void Awake()
    {
        InteractionBounds = transform.FindChild("InteractiveBox").gameObject;
    }

    public void OnTriggerStay(Collider collider)
    {
        HandMotionController.Instance.SetCollider(true, gameObject);
    }

    public void OnTriggerExit(Collider collider)
    {
        HandMotionController.Instance.SetCollider(false, null);
    }
}
