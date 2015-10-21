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
            CurrentPhase = -1;
            NextPhase();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Application.LoadLevel(Application.loadedLevel + 1);
            }
        }

        public void NextPhase()
        {
            var nextPhase = CurrentPhase + 1;
            _tempCollection = _objectCollection;
            _objectCollection = Instantiate(Phases[nextPhase]);
            CurrentPhase += 1;
            print("Current Phase is now " + CurrentPhase);
        }

        public void ClearPreviousPhase(List<Transform> saveList)
        {
                var phase = new GameObject { name = "Phase" + (CurrentPhase - 1) };
                foreach (Transform item in saveList)
                {
                    item.SetParent(phase.transform, true);
                }
                phase.transform.SetParent(transform);
            Destroy(_tempCollection);
        }

        public void ClearPreviousPhase()
        {
            Destroy(_tempCollection);
        }
    }
}
