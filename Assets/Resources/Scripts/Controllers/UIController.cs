using Assets.Scenes.Slides.Resources;
using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        private Cursor _cursor;
        private CursorPresentation _cursorPresentation;
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
            if (_cursor == null)
            {
                _cursorPresentation = CursorObject.GetComponent<CursorPresentation>();
            }
        }

        private void Update()
        {
            if (Application.loadedLevelName.Contains("slide"))
            {
                _cursorPresentation.enabled = IsActive;
            }
            else
            {
                _cursor.enabled = IsActive;
            }
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