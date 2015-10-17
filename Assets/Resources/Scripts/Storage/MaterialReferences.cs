using UnityEngine;

namespace Assets.Resources.Scripts.Storage
{
    public class MaterialReferences : MonoBehaviour
    {
        public static MaterialReferences Instance;
        public Material LiftOff;
        public Material LiftOn;
        public Material TermainalSmallOff;
        public Material TermainalSmallOn;
        public Material TerminalLargeOff;
        public Material TerminalLargeOn;
        public Material TerminalMediumOff;
        public Material TerminalMediumOn;

        private void Awake()
        {
            if (Instance == null && Instance != this)
            {
                Destroy(gameObject.GetComponent<MaterialReferences>());
                Instance = this;
            }
        }
    }
}