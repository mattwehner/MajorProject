using System.Collections.Generic;
using Assets.Resources.Scripts.Object_Specific;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.TutorialSpecific.Phases
{
    public class PhaseThree : MonoBehaviour
    {
        public Animator Door;
        public TutorialTerminal Terminal;
        public TutorialTerminal DoorTerminal;
        public GameObject Power;
        public GameObject Slot;
        public GameObject Line;
        public GameObject Cell;
        public RawImage Circle;
        public Text Text;

        private bool _triggered;

        void Awake()
        {
            print("Phase three awake");
        }

        void Update()
        {
                Circle.color = (Terminal.PoweredOn)?Settings.Colors.Blue: Settings.Colors.Red;
                Text.color = (Terminal.PoweredOn)?Settings.Colors.Blue: Settings.Colors.Red;
            Text.text = (Terminal.PoweredOn) ? "POWER IS ON" : "POWER IS OFF";
            if (DoorTerminal.IsActive && !_triggered)
            {
                NextPhase();
            }
        }

        public void NextPhase()
        {
            _triggered = true;
            Line.SetActive(true);
            Door.enabled = true;
            var saveItems = new List<Transform> {Door.gameObject.transform.parent.transform, Terminal.transform, DoorTerminal.transform, Power.transform, Cell.transform, Slot.transform};
            TutorialController.Instance.NextPhase();
            TutorialController.Instance.ClearPreviousPhase(saveItems);
            TutorialCamera.Instance.CameraBounds.y = 92.7f;
        }
    }
}
