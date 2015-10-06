using System;
using UnityEngine;

namespace Assets.Scripts.Object_Specific
{
    public class PressurePlate : MonoBehaviour, IPowerer
    {
        public bool PowerOn { get; set; }
        private Animation _animation;
        private Material _material;

        void Awake()
        {
            //_animation = GetComponent<Animation>();
            _material = GetComponent<Renderer>().material;

            _material.SetColor("_Color", new Color32(248, 47, 47, 255));
        }

        void OnTriggerEnter()
        {
            PowerOn = true;
            print(name + "was turned on");
            //_animation.Play("pressure_plate_down");
            _material.SetColor("_Color", new Color32(56, 204, 56, 255));
        }

        void OnTriggerStay()
        {
            PowerOn = true;
        }

        void OnTriggerExit()
        {
            PowerOn = false;
            print(name + "was turned off");
            //_animation.Play("pressure_plate_up");
            _material.SetColor("_Color", new Color32(248, 47, 47, 255));
        }
    }
}
