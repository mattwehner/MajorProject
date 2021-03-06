﻿using System.Collections;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using Assets.Resources.Scripts.Storage;
using Assets.Scripts;
using Assets.Scripts.Object_Specific;
using UnityEngine;

namespace Assets.Resources.Scripts.Object_Specific
{
    public class Lift : MonoBehaviour, IInteractable, IPowered, IUiOwner
    {
        public bool PowerOnOveride;
        public GameObject PoweredBy;
        public float[] LiftPositions;
        private GameObject _uiPanel;

        public GameObject InteractionBounds { get; set; }
        public bool IsActive { get; set; }
        public bool PoweredOn { get; set; }

        private IPowerer _iPowerer;
        private bool _isMoving;

        private Vector3 _startCoordinates;
        private float _startTime;
        private float _moveAmount;
        private Vector3 _moveTo;
        private Vector3 _moveFrom;

        private Material _liftOn;
        private Material _liftOff;

        private MeshRenderer _material;
        private bool _hasJumped;

        void Awake()
        {
            _material = GetComponent<MeshRenderer>();
            PoweredBy = (PoweredBy)
                ? PoweredBy
                : gameObject;
            _iPowerer = PoweredBy.GetComponent<IPowerer>();

            InteractionBounds = transform.FindChild("InteractiveBox").gameObject;
            InteractionBounds.SetActive(false);

            ObjectRefences.Instance.MaterialReferenceList.TryGetValue("Lift_On", out _liftOn);
            ObjectRefences.Instance.MaterialReferenceList.TryGetValue("Lift_Off", out _liftOff);

            _startCoordinates = transform.localPosition;
        }

        void Update()
        {
            PoweredOn = (_iPowerer == null)
                ? PowerOnOveride
                : _iPowerer.PowerOn;
            _material.material = (PoweredOn) ? _liftOn : _liftOff;

            if (_isMoving && !PoweredOn)
            {
                _isMoving = false;
            }

            if (_isMoving)
            {
                float distCovered = (Time.time - _startTime) * Settings.Game.LiftSpeed;
                float fracJourney = distCovered / _moveAmount;
                transform.localPosition = Vector3.Lerp(_moveFrom, _moveTo, fracJourney);
                _isMoving = (Vector3.Distance(_moveFrom, _moveTo) > 0.1);
            }
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
                    _uiPanel = Instantiate(UnityEngine.Resources.Load("Prefabs/UI/Lift_Menu")) as GameObject;
                    _uiPanel.transform.SetParent(UIController.Instance.gameObject.transform, false);
                    _uiPanel.transform.SetAsFirstSibling();
                    _uiPanel.GetComponent<UIPanel>().Owner = gameObject.GetComponent<IUiOwner>();
                    InteractionBounds.SetActive(false);
            }
            else
            {
                _uiPanel = Instantiate(UnityEngine.Resources.Load("Prefabs/UI/NoPowerWarning")) as GameObject;
                ;
                _uiPanel.transform.SetParent(gameObject.transform, true);
                _uiPanel.transform.localPosition = new Vector3(0.5f, -0.9f,2);
                InteractionBounds.SetActive(false);
                StartCoroutine(HidePowerUI());
            }
        }

        public void JumpToPosition(float jumpTo)
        {
            if (_hasJumped)
            {
                return;
            }
            _hasJumped = true;
            var jp = transform.localPosition;
            jp.y = jumpTo;
            transform.localPosition = jp;
            OnUiButtonPress("Up");
            
        }

        public void OnUiButtonPress(string pressed)
        {
            var lp = LiftPositions;
            Vector3 _from = transform.localPosition;
            Vector3 _to = _startCoordinates;
            _to.y = (pressed == "Up") ? lp[0] : lp[1];

            _moveAmount = Vector3.Distance(_from, _to);
            _moveTo = _to;
            _moveFrom = _from;
            _startTime = Time.time;

            _isMoving = true;
        }

        IEnumerator HidePowerUI()
        {
            yield return new WaitForSeconds(3);
            Destroy(_uiPanel);
        }
    }
}
