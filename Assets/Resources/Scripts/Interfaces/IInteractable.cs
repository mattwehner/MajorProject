using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Object_Specific
{
    public interface IInteractable
    {
        GameObject InteractionBounds { set; }
        bool IsActive { get; set; }
        void OnTriggerStay(Collider collider);
        void OnTriggerExit(Collider collider);
        void Activate();
    }
}
