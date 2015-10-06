using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.Storage
{
    public class MaterialReferences : MonoBehaviour
    {
        public static MaterialReferences Instance;

        public Material LiftOn;
        public Material LiftOff;

        public Material TermainalSmallOn;
        public Material TermainalSmallOff;

        public Material TerminalMediumOn;
        public Material TerminalMediumOff;

        public Material TerminalLargeOn;
        public Material TerminalLargeOff;

        void Awake()
        {
            if (Instance == null && Instance != this)
            {
                Destroy(gameObject.GetComponent<MaterialReferences>());
                Instance = this;
            }
        }
    }
}
