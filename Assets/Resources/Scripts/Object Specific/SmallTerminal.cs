using System;
using System.Collections;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Assets.Resources.Scripts.Storage;
using Assets.Scripts;
using Assets.Scripts.Object_Specific;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Resources.Scripts.Object_Specific
{
    public class SmallTerminal : MonoBehaviour, IInteractable, IPowered, IUiOwner
    {
        [Serializable]
        public class TerminalAction : UnityEvent { }
        public TerminalAction OnActivation;

        public bool PowerOnOveride;
        public GameObject PoweredBy;

        public GameObject InteractionBounds { get; set; }
        public bool IsActive { get; set; }
        public bool PoweredOn { get; set; }

        private GameObject _uiPanel;
        private IPowerer _iPowerer;
        private MeshRenderer _material;
        private Material _powerOn;
        private Material _powerOff;

        void Awake()
        {
            _material = GetComponent<MeshRenderer>();
            PoweredBy = (PoweredBy)
                ? PoweredBy
                : gameObject;
            _iPowerer = PoweredBy.GetComponent<IPowerer>();

            InteractionBounds = transform.FindChild("InteractiveBox").gameObject;
            InteractionBounds.SetActive(false);
            _powerOff = MaterialReferences.Instance.TermainalSmallOff;
            _powerOn = MaterialReferences.Instance.TermainalSmallOn;
        }

        void Update()
        {
            PoweredOn = (_iPowerer == null)
                ? PowerOnOveride
                : _iPowerer.PowerOn;

            _material.material = (PoweredOn) ? _powerOn : _powerOff;
        }

        public void OnTriggerStay(Collider collider)
        {
            if (collider.name.StartsWith("bone"))
            {
                HandMotionController.Instance.SetCollider(true, gameObject);
                InteractionBounds.SetActive(true);
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            if (collider.name.StartsWith("bone"))
            {
                HandMotionController.Instance.SetCollider(false, null);
            }
            InteractionBounds.SetActive(false);
        }

        public void Activate()
        {
            Destroy(_uiPanel);
            if (PoweredOn)
            {
                InteractionBounds.SetActive(false);
                OnActivation.Invoke();
                UIController.Instance.CursorModeOn(false);
            }
            else
            {
                _uiPanel = Instantiate(UnityEngine.Resources.Load("Prefabs/UI/NoPowerWarning")) as GameObject;
                ;
                _uiPanel.transform.SetParent(gameObject.transform, true);
                _uiPanel.transform.localPosition = Vector3.up * 2;
                InteractionBounds.SetActive(false);
                StartCoroutine(HidePowerUI());
            }
        }

        public void OnUiButtonPress(string pressed)
        {
            throw new NotImplementedException();
        }

        IEnumerator HidePowerUI()
        {
            yield return new WaitForSeconds(3);
            Destroy(_uiPanel);
        }
    }
}
