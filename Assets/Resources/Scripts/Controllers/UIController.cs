using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        private Cursor _cursor;
        public GameObject CursorObject;
        internal bool IsActive;

        private void Awake()
        {
            if (Instance != this || !Instance)
            {
                Destroy(Instance);
                Instance = this;
            }
            _cursor = CursorObject.GetComponent<Cursor>();
        }

        private void Update()
        {
            _cursor.enabled = IsActive;
            CursorObject.SetActive(IsActive);
        }

        public void CursorModeOn(bool active)
        {
            var handController = GameObject.FindGameObjectWithTag("GameController").GetComponent<HandController>();
            if (active)
            {
                handController.DestroyAllHands();
            }

            handController.enabled = !active;
            IsActive = active;
        }
    }
}