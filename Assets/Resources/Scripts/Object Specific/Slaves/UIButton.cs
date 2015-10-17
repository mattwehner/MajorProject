using Assets.Resources.Scripts.Interfaces;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Object_Specific.Slaves
{
    public class UIButton : MonoBehaviour, IUiButton
    {
        private bool _beenActivated;
        private RawImage _rawImage;

        public void OnTriggerEnter2D()
        {
            _rawImage.color = Settings.Colors.Hover;
        }

        public void OnTriggerExit2D()
        {
            _rawImage.color = Color.white;
            _beenActivated = false;
        }

        public void ButtonAction()
        {
            if (!_beenActivated)
            {
                _beenActivated = true;
                transform.parent.GetComponent<UIPanel>().Owner.OnUiButtonPress(gameObject.name);
                _rawImage.color = Settings.Colors.Selected;
            }
        }

        private void Awake()
        {
            _rawImage = GetComponent<RawImage>();
        }
    }
}