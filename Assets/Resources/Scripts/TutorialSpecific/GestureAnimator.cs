using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.TutorialSpecific
{
    public class GestureAnimator : MonoBehaviour
    {
        public List<Texture2D> SpriteList;
        public bool Animate;

        private RawImage _hand;
        private int _current;
        private float _switchTime;

        void Awake()
        {
            _switchTime = Time.timeSinceLevelLoad;
            _hand = GetComponent<RawImage>();
        }
        void Update()
        {
            if (Time.timeSinceLevelLoad > _switchTime + 0.5 && Animate)
            {
                _hand.texture = SpriteList[_current];
                _current = (_current == SpriteList.Count -1) ? 0 : _current + 1;
                _switchTime = Time.timeSinceLevelLoad;
            }
        }
    }
}
