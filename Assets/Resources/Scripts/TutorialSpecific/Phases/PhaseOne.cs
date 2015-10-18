using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.TutorialSpecific.Phases
{
    public class PhaseOne : MonoBehaviour
    {
        public GameObject Title;
        public GameObject Instructions1;
        public GameObject Instructions2;
        public GameObject Line;
        public RawImage HandAnimation;
        public Texture2D Hand1;
        public Texture2D Hand2;
        public static bool HasReachedWaypoint;

        private float _awakeTime;
        private bool _clearedPhase;

        private bool _animateHand;
        private bool _isHand1;
        private float _switchTime;

        void Awake()
        {
            _awakeTime = Time.timeSinceLevelLoad;
            Title.SetActive(true);
            Instructions1.SetActive(false);
            Instructions2.SetActive(false);
            Line.SetActive(false);
            print("Phase One Started");
        }
        void Update ()
        {
            if (TutorialController.Instance.CanSetWaypoint && HasReachedWaypoint)
            {
                Line.SetActive(true);
                TutorialController.Instance.NextPhase();
                TutorialCamera.Instance.CameraBounds.y = 60;
            }
            if (Time.timeSinceLevelLoad > _switchTime + 0.5 && _animateHand)
            {
                HandAnimation.texture = (_isHand1) ?Hand2: Hand1;
                _isHand1 = !_isHand1;
                _switchTime = Time.timeSinceLevelLoad;
            }
            if (!_animateHand)
            {
                if (Time.timeSinceLevelLoad > (_awakeTime + 7) && !_clearedPhase)
                {
                    TutorialController.Instance.ClearPreviousPhase();
                    Title.SetActive(false);
                    Instructions1.SetActive(true);
                    _clearedPhase = true;
                }
                if (Instructions1.activeSelf && TutorialGestures.Instance.IsPointing)
                {
                    Instructions2.SetActive(true);
                }
                if (Instructions2.activeSelf)
                {
                    _animateHand = true;
                    TutorialController.Instance.CanSetWaypoint = true;
                }
            }
        }
    }
}
