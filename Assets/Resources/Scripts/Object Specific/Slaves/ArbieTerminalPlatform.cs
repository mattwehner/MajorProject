using UnityEngine;

namespace Assets.Resources.Scripts.Object_Specific.Slaves
{
    public class ArbieTerminalPlatform : MonoBehaviour
    {
        private ArbieTerminal _arbieTerminal;

        private void Awake()
        {
            _arbieTerminal = transform.parent.GetComponent<ArbieTerminal>();
        }

        private void OnTriggerStay(Collider collider)
        {
            if (collider.tag == "Arbie")
            {
                _arbieTerminal.ArbiePresent = true;
            }
        }
    }
}