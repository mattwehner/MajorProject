using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Scripts.Object_Specific;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Resources.Scripts.TutorialSpecific.Phases
{
    class PhaseFour : MonoBehaviour
    {
        public Animator Door;
        public TutorialTerminal DoorTerminal;
        public GameObject Line;

        private bool _triggered;

        void Update()
        {
            if (DoorTerminal.IsActive && !_triggered)
            {
                NextPhase();
            }
        }

        public void NextPhase()
        {
            _triggered = true;
            Door.enabled = true;
            Line.SetActive(true);
            TutorialCamera.Instance.CameraBounds.y = 105f;
        }
    }
}
