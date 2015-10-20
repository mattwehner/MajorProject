using System.Collections.Generic;
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
        public GestureAnimator HandAnimation;
        public static bool HasReachedWaypoint;

        private float _awakeTime;

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
        }
        void Update ()
        {
            if (TutorialController.Instance.CurrentPhase != 1)
            {
                return;
            }
            if (TutorialController.Instance.CanSetWaypoint && HasReachedWaypoint)
            {
                Line.SetActive(true);
                var saveItems = new List<Transform> { Instructions1.transform, Instructions2.transform, Line.transform };
                TutorialController.Instance.NextPhase();
                TutorialController.Instance.ClearPreviousPhase(saveItems);
                TutorialCamera.Instance.CameraBounds.y = 46f;
            }
            if (!HandAnimation.Animate)
            {
                if (Time.timeSinceLevelLoad > (_awakeTime + 7))
                {
                    Instructions1.SetActive(true);
                }
                if (Instructions1.activeSelf && TutorialGestures.Instance.IsPointing)
                {
                    Instructions2.SetActive(true);
                }
                if (Instructions2.activeSelf)
                {
                    HandAnimation.Animate = true;
                    TutorialController.Instance.CanSetWaypoint = true;
                }
            }
        }
    }
}
