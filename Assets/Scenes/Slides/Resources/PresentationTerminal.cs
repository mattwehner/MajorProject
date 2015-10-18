using System;
using System.Collections;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Assets.Resources.Scripts.Storage;
using Assets.Scenes.Slides.Resources;
using Assets.Scripts;
using Assets.Scripts.Object_Specific;
using UnityEngine;

namespace Assets.Resources.Scripts.Object_Specific
{
    public class PresentationTerminal : MonoBehaviour, IInteractable, IPowered, IUiOwner
    {
        public SelectGesture SelectGesture;
        public GameObject PresentationCanvas;

        private IPowerer _iPowerer;
        private MeshRenderer _material;
        private Material _powerOff;
        private Material _powerOn;
        private GameObject _uiPanel;
        public GameObject PoweredBy;
        public bool PowerOnOveride;
        public int TerminalType;
        public string TerminalUI;
        public GameObject InteractionBounds { get; set; }
        public bool IsActive { get; set; }
        public bool PoweredOn { get; set; }

        public void OnTriggerStay(Collider collider)
        {
            if (collider.name.StartsWith("bone"))
            {
                SelectGesture.SetCollider(true, gameObject);
                InteractionBounds.SetActive(true);
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            if (collider.name.StartsWith("bone"))
            {
                SelectGesture.SetCollider(false, null);
            }
            InteractionBounds.SetActive(false);
        }

        public void Activate()
        {
            Destroy(_uiPanel);
            if (PoweredOn)
            {
                _uiPanel = Instantiate(UnityEngine.Resources.Load("Prefabs/UI/" + TerminalUI)) as GameObject;
                ;
                _uiPanel.transform.SetParent(PresentationCanvas.transform, false);
                _uiPanel.transform.SetAsFirstSibling();
                _uiPanel.GetComponent<UIPanel>().Owner = gameObject.GetComponent<IUiOwner>();
                InteractionBounds.SetActive(false);
            }
        }

        public void OnUiButtonPress(string pressed)
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>();
            PoweredBy = (PoweredBy)
                ? PoweredBy
                : gameObject;
            _iPowerer = PoweredBy.GetComponent<IPowerer>();

            InteractionBounds = transform.FindChild("InteractiveBox").gameObject;
            InteractionBounds.SetActive(false);
            
                    ObjectRefences.Instance.MaterialReferenceList.TryGetValue("Console_Large_Broken", out _powerOn);
                    ObjectRefences.Instance.MaterialReferenceList.TryGetValue("Console_Large_Broken", out _powerOff);
        }

        private void Update()
        {
            PoweredOn = (_iPowerer == null)
                ? PowerOnOveride
                : _iPowerer.PowerOn;

            _material.material = (PoweredOn) ? _powerOn : _powerOff;
        }

        private IEnumerator HidePowerUI()
        {
            yield return new WaitForSeconds(3);
            Destroy(_uiPanel);
        }
    }
}