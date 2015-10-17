using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Object_Specific.UI
{
    public class Tasks : MonoBehaviour
    {
        public Monitor Owner;
        public RawImage Task1;
        private Text Task1Text;
        public RawImage Task2;

        private void Awake()
        {
            Task1Text = Task1.transform.GetChild(0).GetComponent<Text>();
            Task1.color = Color.red;
            Task2.color = Color.red;
        }

        private void LateUpdate()
        {
            Task1.color = (Owner.Task1) ? Color.green : Color.red;
            Task1Text.text = (Owner.Task1) ? "RESTORED POWER" : "1. RESTORE POWER";
            Task2.color = (Owner.Task2) ? Color.green : Color.red;
        }
    }
}