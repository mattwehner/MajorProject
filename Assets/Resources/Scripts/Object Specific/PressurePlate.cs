using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Object_Specific
{
    public class PressurePlate : MonoBehaviour, IPowerer
    {
        private Animator _animator;
        private bool _canAnimate;
        private Material _material;
        public bool TriggerOnce;
        public bool PowerOn { get; set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _material = GetComponent<Renderer>().material;

            _material.SetColor("_Color", new Color32(248, 47, 47, 255));
        }

        private void Start()
        {
            _animator.Play("pressure_plate_up");
        }

        private void OnTriggerEnter()
        {
            print("trigger enter");
            _animator.Play("pressure_plate_down");
            StartCoroutine(DelayTrigger());
        }

        private void OnTriggerExit()
        {
            StopAllCoroutines();
            print("trigger exit");
            if (TriggerOnce) return;
            PowerOn = false;
            _animator.Play("pressure_plate_up");
            _material.SetColor("_Color", new Color32(248, 47, 47, 255));
            print(name + "was turned off");
        }

        private IEnumerator DelayTrigger()
        {
            yield return new WaitForSeconds(0.5f);
            _material.SetColor("_Color", new Color32(56, 204, 56, 255));
            print(name + "was turned on");
            PowerOn = true;
        }
    }
}