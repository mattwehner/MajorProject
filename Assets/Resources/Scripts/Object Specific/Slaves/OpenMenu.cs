using Assets.Resources.Scripts.Controllers;
using Assets.Resources.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Resources.Scripts.Object_Specific.Slaves
{
    public class OpenMenu : MonoBehaviour, IMenuActioner
    {
        private float _startTime;
        public bool IsActive { get; set; }

        void Update()
        {
            if (IsActive && (_startTime + 1 < Time.time))
            {
                UIController.Instance.CursorModeOn(true);
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            _startTime = Time.time;
            IsActive = true;
        }
    

        void OnTriggerExit2D()
        {
            _startTime = 0;
            IsActive = false;
        }
    }
}
