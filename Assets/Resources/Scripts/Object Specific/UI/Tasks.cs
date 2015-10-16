using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Object_Specific.UI
{
    public class Tasks : MonoBehaviour
    {
        public RawImage Task1;
        public RawImage Task2;
        public Monitor Owner;

        private Text Task1Text;

        void Awake()
        {
            Task1Text = Task1.transform.GetChild(0).GetComponent<Text>();
            Task1.color = Color.red;
            Task2.color = Color.red;
        }
        void LateUpdate()
        {
            Task1.color = (Owner.Task1) ? Color.green : Color.red;
                Task1Text.text = (Owner.Task1)?"RESTORED POWER":"1. RESTORE POWER";
            Task2.color = (Owner.Task2) ? Color.green : Color.red;
        }
    }
}
