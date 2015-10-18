using Leap;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.TutorialSpecific.Phases
{
    public class PhaseZero : MonoBehaviour
    {
        public GameObject Title;
        public GameObject CameraHint;
        public TextMesh IntroText;
        public GameObject MoveCamera;

        private Frame _frame;

        void Awake()
        {
            Title.SetActive(true);
            IntroText.gameObject.SetActive(true);
            CameraHint.SetActive(false);
            MoveCamera.SetActive(false);
        }

        void Update ()
        {
            _frame = TutorialGestures.Instance.Controller.Frame();
            if (_frame.Hands.IsEmpty || TutorialController.Instance.CurrentPhase != 0) { return;}

            if (Title.activeSelf)
            {
                if (IdealHandHeight())
                {
                    var elements = Title.GetComponentsInChildren<Text>();
                    foreach (Text text in elements)
                    {
                        var alpha = text.color.a;
                        var c = text.color;
                        text.color = new Color(c.r, c.g, c.b, alpha - Time.fixedDeltaTime);
                    }
                    Title.SetActive(elements[0].color.a > -2);
                    IntroText.gameObject.SetActive(elements[0].color.a > -2);
                }
                else
                {
                    var elements = Title.GetComponentsInChildren<Text>();
                    if (elements[0].color.a >= 255)
                    {
                        return;
                    }
                    foreach (Text text in elements)
                    {
                        var alpha = text.color.a;
                        var c = text.color;
                        text.color = new Color(c.r, c.g, c.b, alpha + Time.fixedDeltaTime);
                    }
                }
                return;
            }
            CameraHint.SetActive(true);
            if (TutorialGestures.Instance.IsCamera)
            {
                CameraHint.SetActive(false);
                MoveCamera.SetActive(true);
                TutorialController.Instance.NextPhase();
                TutorialController.Instance.CanUseCamera = true;
            }
        }

        private bool IdealHandHeight()
        {
            var handHeight = _frame.Hands[0].PalmPosition;
            var inState = false;
            if (handHeight.y < 95)
            {
                IntroText.text = "Higher please";
                IntroText.color = Color.red;
            }
            if (handHeight.y > 95 && handHeight.y < 125)
            {
                IntroText.text = "Just a little higher";
                IntroText.color = new Color32(255, 142, 0, 255);
            }
            if (handHeight.y > 195 && handHeight.y < 230)
            {
                IntroText.text = "Just a little lower";
                IntroText.color = new Color32(255, 142, 0, 255);
            }
            if (handHeight.y > 230)
            {
                IntroText.text = "Lower please";
                IntroText.color = Color.red;

            }
            if (handHeight.y < 195 && handHeight.y > 125)
            {
                IntroText.text = "Perfect. Your hand is better tracked here";
                IntroText.color = new Color32(55, 159, 0, 255);
                inState = true;
            }
            return inState;
        }
    }
}
