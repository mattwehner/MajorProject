using Assets.Resources.Scripts.Interfaces;
using Assets.Scripts.Object_Specific;
using UnityEngine;

public class Powercell : MonoBehaviour, IPowered, IGrabable
{
    private bool _previousGrabState;
    public bool BeingGrabbed { get; set; }
    public bool PoweredOn { get; set; }

    private void Update()
    {
        if (BeingGrabbed != _previousGrabState)
        {
            OnGrabStateChange(BeingGrabbed);
        }
    }

    public void OnGrabStateChange(bool state)
    {
        var hc = GetComponent<HandController>();

        hc.IgnoreCollisionsWithHands(gameObject, state);
        _previousGrabState = BeingGrabbed;
    }
}