using UnityEngine;

namespace Assets.Scripts.Object_Specific
{
    public interface IInteractable
    {
        GameObject InteractionBounds { set; }
        bool IsActive { get; set; }
        bool PoweredOn { get; set; }
        void OnTriggerStay(Collider collider);
        void OnTriggerExit(Collider collider);
        void Activate();
    }
}