using System;
using System.Collections;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Assets.Resources.Scripts.Object_Specific.UI;
using Assets.Scripts;
using Assets.Scripts.Object_Specific;
using UnityEngine;

namespace Assets.Resources.Scripts.Object_Specific
{
    public class Monitor : MonoBehaviour, IInteractable, IPowered, IUiOwner
    {

        internal bool Task1;
        internal bool Task2;

        public bool PowerOnOveride;
        public GameObject PoweredBy;

        public GameObject InteractionBounds { get; set; }
        public bool IsActive { get; set; }
        public bool PoweredOn { get; set; }

        private GameObject _uiPanel;
        private IPowerer _iPowerer;
        private MeshRenderer _material;
        public Material _powerOn;
        public Material _powerOff;

        void Awake()
        {
            _material = GetComponent<MeshRenderer>();
            PoweredBy = (PoweredBy)
                ? PoweredBy
                : gameObject;
            _iPowerer = PoweredBy.GetComponent<IPowerer>();
            InteractionBounds = transform.FindChild("InteractiveBox").gameObject;
            InteractionBounds.SetActive(false);
        }

        void Update()
        {
            PoweredOn = (_iPowerer == null)
                ? PowerOnOveride
                : _iPowerer.PowerOn;

            _material.material = (EndGame.PowerRestored) ? _powerOn : _powerOff;

            Task1 = EndGame.PowerRestored;
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
            _uiPanel = Instantiate(UnityEngine.Resources.Load("Prefabs/UI/Tasks")) as GameObject;
            ;
            _uiPanel.transform.SetParent(UIController.Instance.gameObject.transform, false);
            _uiPanel.transform.SetAsFirstSibling();
            _uiPanel.GetComponent<Tasks>().Owner = gameObject.GetComponent<Monitor>();
            InteractionBounds.SetActive(false);
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
