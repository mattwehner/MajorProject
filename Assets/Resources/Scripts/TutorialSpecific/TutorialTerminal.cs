﻿using System;
using System.Collections;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Assets.Resources.Scripts.Storage;
using Assets.Resources.Scripts.TutorialSpecific;
using Assets.Scripts;
using Assets.Scripts.Object_Specific;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Object_Specific
{
    public class TutorialTerminal : MonoBehaviour, IInteractable, IPowered, IUiOwner
    {
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

        public PhaseTwo Phase2;
        public RawImage Activated;

        public void OnTriggerStay(Collider collider)
        {
            if (collider.name.StartsWith("bone"))
            {
                TutorialGestures.Instance.SetCollider(true, gameObject);
                InteractionBounds.SetActive(true);
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            if (collider.name.StartsWith("bone"))
            {
                TutorialGestures.Instance.SetCollider(false, null);
            }
            InteractionBounds.SetActive(false);
        }

        public void Activate()
        {
            if (!IsActive && TutorialController.Instance.CurrentPhase == 2)
            {
                Activated.color = Color.green;
                Phase2.ActivatedTerminals += 1;
                IsActive = true;
                return;
            }
            if(TutorialController.Instance.CurrentPhase != 2) {
                Destroy(_uiPanel);
                if (PoweredOn)
                {
                    IsActive = true;
                }
                else
                {
                    _uiPanel = Instantiate(UnityEngine.Resources.Load("Prefabs/UI/NoPowerWarning")) as GameObject;
                    _uiPanel.transform.SetParent(gameObject.transform, true);
                    _uiPanel.transform.localPosition = Vector3.up * 2;
                    InteractionBounds.SetActive(false);
                    StartCoroutine(HidePowerUI());
                }
            }
        }

        public void OnUiButtonPress(string pressed)
        {
            throw new NotImplementedException();
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

            switch (TerminalType)
            {
                case 1:
                    ObjectRefences.Instance.MaterialReferenceList.TryGetValue("Console_Small_On", out _powerOn);
                    ObjectRefences.Instance.MaterialReferenceList.TryGetValue("Console_Small_Off", out _powerOff);
                    break;
                case 2:
                    ObjectRefences.Instance.MaterialReferenceList.TryGetValue("Console_Medium_On", out _powerOn);
                    ObjectRefences.Instance.MaterialReferenceList.TryGetValue("Console_Medium_Off", out _powerOff);
                    break;
                case 3:
                    ObjectRefences.Instance.MaterialReferenceList.TryGetValue("Console_Large_Broken", out _powerOn);
                    ObjectRefences.Instance.MaterialReferenceList.TryGetValue("Console_Large_Broken", out _powerOff);
                    break;
            }

            Phase2 = Phase2 ?? new PhaseTwo();
            

        }

        void Start()
        {
            if (TutorialController.Instance.CurrentPhase == 2)
            {
                Activated.color = Color.red;
            }
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