using System.Collections.Generic;
using Leap;
using UnityEngine;

namespace Assets.Resources.Scripts.TutorialSpecific
{
    public class TutorialController : MonoBehaviour
    {
        public static TutorialController Instance;
        public List<GameObject> Phases;
        public GameObject Waypoint;
        internal GameObject CurrentWaypoint;

        
        public bool CanUseCursor;
        public bool CanUseCamera;
        public bool CanSetWaypoint;

        public bool InCursorMode;

        private bool _phaseInstantiated;
        private GameObject _objectCollection;
        private GameObject _tempCollection;

        public int CurrentPhase;

        void Awake()
        {
            Instance = this;
            GameObject.FindGameObjectWithTag("Player").transform.position = Vector3.zero;
            CurrentPhase = -1;
            NextPhase();
        }

        public void NextPhase()
        {
            var nextPhase = CurrentPhase + 1;
            _tempCollection = _objectCollection;
            _objectCollection = Instantiate(Phases[nextPhase]);
            CurrentPhase += 1;
            print("Current Phase is now " + CurrentPhase);
        }

        public void ClearPreviousPhase()
        {
            Destroy(_tempCollection);
        }
    }
}
