using System.Collections.Generic;
using Leap;
using UnityEngine;

namespace Assets.Resources.Scripts.TutorialSpecific
{
    public class TutorialController : MonoBehaviour
    {
        public static TutorialController Instance;
        public List<GameObject> Phases;

        
        public bool CanUseCursor;
        public bool CanUseCamera;

        public bool InCursorMode;

        private bool _phaseInstantiated;
        private GameObject _objectCollection;

        public int CurrentPhase;
        private int _completedPhase;

        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (CurrentPhase == _completedPhase)
            {
                _objectCollection = Instantiate(Phases[0]);
                CurrentPhase += 1;
            }
        }
    }
}
