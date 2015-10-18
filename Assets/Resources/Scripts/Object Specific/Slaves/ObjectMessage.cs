using Assets.Resources.Scripts.Controllers;
using UnityEngine;

namespace Assets.Resources.Scripts.Object_Specific.Slaves
{
    public class ObjectMessage : MonoBehaviour
    {
        public int Message;
        public int TriggerAmount;
        public float WaitBetweenMessages;

        private int triggerCount;
        private float lastTriggered;

        void Awake()
        {
            TriggerAmount = (TriggerAmount == 0) ? 1000 : TriggerAmount;
            WaitBetweenMessages = (WaitBetweenMessages < 5) ? 5 : WaitBetweenMessages;
        }
        void OnTriggerEnter(Collider collider)
        {
            if (collider.name.Contains("Arbie")
                && (Time.timeSinceLevelLoad > (lastTriggered + WaitBetweenMessages))
                && triggerCount <= TriggerAmount
                )
            {
                collider.GetComponent<ArbieController>().PlayMessage(Message);
                lastTriggered = Time.timeSinceLevelLoad;
                triggerCount += 1;
            }
        }
    }
}
