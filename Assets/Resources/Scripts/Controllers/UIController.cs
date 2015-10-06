using System.Collections.Generic;
using Assets.Scripts.Object_Specific;
using Leap;
using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        public GameObject CursorObject;
        private Cursor _cursor;

        internal bool IsActive;
        void Awake()
        {
            if (Instance != this || !Instance)
            {
                Destroy(Instance);
                Instance = this;
            }
            _cursor = CursorObject.GetComponent<Cursor>();
        }

        void Update()
        {
            _cursor.enabled = IsActive;
            CursorObject.SetActive(IsActive);
        }

        public void CursorModeOn(bool active)
        {
            HandController handController = GameObject.FindGameObjectWithTag("GameController").GetComponent<HandController>();
            if (active)
            {
                handController.DestroyAllHands();
            }

            handController.enabled = !active;
            IsActive = active;
        }
    }
}
