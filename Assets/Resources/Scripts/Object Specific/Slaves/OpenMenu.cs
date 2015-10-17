using Assets.Resources.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Resources.Scripts.Object_Specific.Slaves
{
    public class OpenMenu : MonoBehaviour, IMenuActioner
    {
        public bool SwitchCursor { get; set; }

        private void OnTriggerStay2D()
        {
            SwitchCursor = false;
        }

        private void OnTriggerExit2D()
        {
            SwitchCursor = true;
        }
    }
}