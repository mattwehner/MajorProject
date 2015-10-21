using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Object_Specific;
using Assets.Resources.Scripts.Object_Specific.Slaves;
using Assets.Scripts;
using Assets.Scripts.Object_Specific;
using UnityEngine;

namespace Assets.Resources.Scripts.TutorialSpecific.Phases
{
    class PhaseFive : MonoBehaviour, IInteractable
    {
        public Animator Door;
        public GameObject InteractionBounds { get; set; }
        public NavMeshAgent Arbie;
        public bool IsActive { get; set; }
        public bool PoweredOn { get; set; }

        private bool _moveToPosition;
        private GameObject _arbie;
        private bool _canSet;

        void Awake()
        {
            InteractionBounds = transform.FindChild("InteractiveBox").gameObject;
            InteractionBounds.SetActive(false);
            _arbie = GameObject.FindGameObjectWithTag("Arbie");
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Arbie")
            {
                _canSet = true;
                ArbieController.Instance.NavAgentEnabled(false);
            }
        }

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
            Door.enabled = true;
            TutorialWaypoint.Instance.Delete();
            ArbieController.Instance.NavAgentEnabled(true);
            TutorialWaypoint.Instance.Create(new Vector3(112.4f, -2.86f, 2.2f));
            TutorialController.Instance.CanSetWaypoint = false;
        }

        void Update()
        {
            _moveToPosition = (_arbie.transform.localPosition.x > 102);
            if (_moveToPosition && _canSet)
            {
                _arbie.transform.localPosition = Vector3.Lerp(_arbie.transform.localPosition,
                    new Vector3(102.381f, -2.871659f, 5.17f), 1*Time.deltaTime);

                if (_arbie.transform.position.z < 3.6f)
                {
                    Door.Play("Close");
                    StartCoroutine(Utilities.WaitFor(3f, LoadLevel));
                }
            }
        }

        private void LoadLevel()
        {
            Application.LoadLevel("Level1");
        }
    }
}
