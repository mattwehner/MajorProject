using System.Collections;
using System.Threading;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Resources.Scripts.Object_Specific
{
    public class EndGame : MonoBehaviour
    {
        public GameObject Camera;
        private bool _hasCalled;

        void OnTriggerEnter(Collider collider)
        {
            if (collider.name == "Arbie")
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        void Awake()
        {
            Camera.SetActive(false);
        }

        public void MoveCameraTo(int position)
        {
            if (!_hasCalled)
            {
                StartCoroutine(MoveCamera(1));
            }
        }

        IEnumerator MoveCamera(int position)
        {
            Camera.SetActive(true);
            yield return new WaitForSeconds(3f);
            Camera.SetActive(false);
            _hasCalled = true;
        }
    }
}
