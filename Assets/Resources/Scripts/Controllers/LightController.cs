using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    [ExecuteInEditMode]
    public class LightController : MonoBehaviour
    {

        public List<GameObject> LightList;
        public Material Material;
        private List<Light> _lights;
        public float OnValue;
        public bool AnimationPlaying;

        private bool _lightsOn;
        private Animator _animator;
        private Color32 _onColor;
        private Color32 _offColor;
        private float _untilPlay;

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
            CalculateAnimation();
            if (!EndGame.PowerRestored)
            {
                CaluculateLightEffects();
            }
            else
            {
                OnValue = 1;
                CaluculateLightEffects();
            }
        }

        void CaluculateLightEffects()
        {
            _lightsOn = OnValue > 0.05f;

            foreach (Light item in _lights)
            {
                var value = Mathf.Clamp(OnValue, 0.6f, 1);
                if (!_lightsOn)
                {
                    item.intensity = 4;
                }
                else
                {
                    item.intensity = (item.Equals(_lights[4])) ? value + 0.2f : value;
                }
                item.color = (_lightsOn) ? Settings.Colors.Blue : Settings.Colors.Red;
            }

            if (!_lightsOn)
            {
                Material.SetColor("_EmissionColor", Settings.Colors.Red);
            }
            else
            {
                Material.SetColor("_EmissionColor", Color32.Lerp(Color.black, Settings.Colors.Blue, OnValue));
            }
            RenderSettings.ambientLight = Color32.Lerp(_offColor, _onColor, OnValue);
        }

        void CalculateAnimation()
        {
            if (AnimationPlaying)
            {
                return;
            }
            if (Time.timeSinceLevelLoad > _untilPlay)
            {
                _untilPlay = Time.timeSinceLevelLoad + (Random.Range(30, 60));
                print("Time until next animation play: " + (_untilPlay - Time.timeSinceLevelLoad));
                float range = Random.Range(0, 5);
                print(range);
                var name = (range < 3) ? "Light_Off" : "Level_Flicker";
                _animator.PlayInFixedTime(name);
                print("Playing Animation: " + name);
            }
        }
    }
}
