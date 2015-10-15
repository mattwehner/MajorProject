using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    public class LightController : MonoBehaviour
    {

        public List<GameObject> LightList;
        public Material Material;
        private List<Light> _lights;
        public float OnValue;

        private bool _lightsOn;
        private Animator _animator;
        private Color32 _onColor;
        private Color32 _offColor;

        void Awake ()
        {
            _animator = GetComponent<Animator>();
            _onColor = Settings.Colors.AmbientLightOn;
            _offColor = Settings.Colors.AmbientLightOff;

            _lights = new List<Light>();
            foreach (GameObject item in LightList)
            {
                _lights.Add(item.GetComponent<Light>());
            }
        }

        void Update ()
        {
            OnValue = Mathf.Clamp(OnValue, 0, 1);
            if (OnValue < 0.1)
            {
                _lightsOn = false;
            }else if (OnValue > 0.9)
            {
                _lightsOn = true;
            }

            foreach (Light item in _lights)
            {
                var value = Mathf.Clamp(OnValue, 0.6f, 1);
                item.intensity = (item.Equals(_lights[4]))? value+0.2f: value;
                item.color = (_lightsOn) ? Settings.Colors.Blue : Settings.Colors.Red;
            }

            if (_lightsOn)
            {
                Material.SetColor("_EmissionColor", Settings.Colors.Blue);
            }
            else
            {
                Material.SetColor("_EmissionColor", Settings.Colors.Red);
            }
            RenderSettings.ambientLight = Color32.Lerp(_offColor, _onColor, OnValue);
        }
    }
}
