using System;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Assets.Resources.Scripts.Object_Specific.Slaves;
using Assets.Scripts;
using Assets.Scripts.Object_Specific;
using Leap;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.Slides.Resources
{
    public class SelectGesture : MonoBehaviour
    {
        public GameObject Waypoint;
        public Controller Controller;
        internal bool IsActive;
        private Frame _frame;
        private Hand _hand;
        private float _heightOffset;
        private IMenuActioner _iMenuActioner;
        private Frame _storedFrame;

        internal bool InCollider;
        internal GameObject Triggering;

        private void Awake()
        {
            Controller = new Controller();
            _storedFrame = new Frame();
            Waypoint.SetActive(false);
        }

        private void Update()
        {
            _frame = Controller.Frame();
            _hand = _frame.Hands[0];
            _storedFrame = (_frame.Id > (_storedFrame.Id + 10)) ? _frame : _storedFrame;
            
            if (HasSelectedCheck() && !UIController.Instance.IsActive)
            {
                HasClicked();
            }
        }

        private bool HasSelectedCheck()
        {
            var _extendedFingers = _storedFrame.Hands[0].Fingers.Extended();
            var _thumb = _storedFrame.Hands[0].Fingers.FingerType(Finger.FingerType.TYPE_THUMB)[0];
            var _index = _storedFrame.Hands[0].Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];

            var before = (_extendedFingers[0].Id == _index.Id || _extendedFingers[1].Id == _index.Id)
                         && (_extendedFingers[0].Id == _thumb.Id || _extendedFingers[1].Id == _thumb.Id)
                         && (_extendedFingers.Count == 2);

            var extendedFingers = _frame.Hands[0].Fingers.Extended();
            var index = _hand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX)[0];

            var now = (extendedFingers[0].Id == index.Id || extendedFingers[1].Id == index.Id)
                      && (extendedFingers.Count == 1);
            return (before && now);
        }

        private void HasClicked()
        {
            var inCollider = InCollider;
            var triggering = Triggering;

            if (!inCollider && triggering == null)
            {
                Waypoint.SetActive(true);
                StartCoroutine(Utilities.WaitFor(3, HideWaypoint));
            }

            if (inCollider && Triggering != null)
            {
                IInteractable interactable = Triggering.GetComponent<IInteractable>();
                interactable.Activate();
                UIController.Instance.CursorModeOn(true);
            }
        }

        public void CursorModeOn(bool active)
        {
            var handController = GameObject.FindGameObjectWithTag("GameController").GetComponent<HandController>();
            if (active)
            {
                handController.DestroyAllHands();
            }

            handController.enabled = !active;
            IsActive = active;
        }

        public void SetCollider(bool b, GameObject o)
        {
            InCollider = b;
            Triggering = o;
        }

        private void HideWaypoint()
        {
            Waypoint.SetActive(false);
        }
    }
}
