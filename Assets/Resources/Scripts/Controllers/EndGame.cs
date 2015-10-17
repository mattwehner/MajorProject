using System.Collections;
using Assets.Resources.Scripts.Object_Specific.Slaves;
using UnityEngine;

namespace Assets.Resources.Scripts.Controllers
{
    public class EndGame : MonoBehaviour
    {
        public GameObject Camera;
        public GameObject Player;
        public GameObject Platform;
        public GameObject EndScreen;
        public static bool PowerRestored;

        private bool _hasCalled;
        private CameraController _cameraController;
        private GameObject _handController;

        void OnTriggerEnter(Collider collider)
        {
            if (collider.name == "Arbie" && PowerRestored)
            {
                Player.transform.SetParent(Platform.transform);
                _cameraController.enabled = false;
                var hands = _handController.GetComponent<HandController>();
                hands.DestroyAllHands();
                _handController.SetActive(false);
                StartCoroutine(Utilities.WaitFor(3f, PlayEndScreen));
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name == "Arbie" && !PowerRestored)
            {
                Destroy(collision.gameObject);
                GameController.Instance.SpawnArbie();
                MoveCameraTo(2);
            }
        }

        void Awake()
        {
            Camera.SetActive(false);
            _handController = Player.transform.GetChild(2).gameObject;
            _cameraController = Player.GetComponent<CameraController>();
        }

        public void RestorePower()
        {
            PowerRestored = true;
        }

        public void MoveCameraTo(int position)
        {
            if (!_hasCalled)
            {
                StartCoroutine(MoveCamera(position));
            }
        }

        IEnumerator MoveCamera(int position)
        {
            var position1 = new Vector3(-16.08242f, 1.73f, 7.735077f);
            var position2 = new Vector3(8.43f, 2.94f, 0.34f);
            switch (position)
            {
                case 1:
                    Camera.transform.localPosition= position1;
                    break;
                case 2:
                    Camera.transform.localPosition = position2;
                    Camera.transform.Rotate(0.34f, 0,0);
                    break;
            }
            Camera.SetActive(true);
            yield return new WaitForSeconds(3f);
            if (position == 2)
            {
                Camera.transform.Rotate(-0.34f, 0, 0);
            }
            Camera.SetActive(false);
            _hasCalled = true;
        }

        private void PlayEndScreen()
        {
            EndScreen.SetActive(true);
            EndScreen.GetComponent<Animator>().Play("FadeIn");
        }
    }
}
