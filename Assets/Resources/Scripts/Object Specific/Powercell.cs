using UnityEngine;
using System.Collections;
using Assets.Resources.Scripts.Interfaces;
using Assets.Scripts.Object_Specific;

public class Powercell : MonoBehaviour, IPowered, IGrabable {
    public bool PoweredOn { get; set; }
    public bool BeingGrabbed { get; set; }
    private bool _previousGrabState;

    void Update () {

        if (BeingGrabbed != _previousGrabState)
        {
            OnGrabStateChange(BeingGrabbed);
        }
    }
    
    public void OnGrabStateChange(bool state)
    {
        var hc = GetComponent<global::HandController>();

        hc.IgnoreCollisionsWithHands(gameObject, state);
        _previousGrabState = BeingGrabbed;
    }
}
