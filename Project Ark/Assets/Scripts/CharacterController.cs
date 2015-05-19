using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CharacterController : MonoBehaviour
    {
        private GameObject _arbie;

        private bool hasBeenThrown = false;
        void Start()
        {
            Debug.Log("CharacterController Is Alive");
            _arbie = PublicReferenceList.Character;
        }

        void Update()
        {
            if (hasBeenThrown && (_arbie.GetComponent<Rigidbody>().velocity.magnitude <= Settings.Game.CharacterRecoverVelocity))
            {
                StartCoroutine(WaitFor(5));
                Debug.Log("I'm Back");
                hasBeenThrown = false;
                _arbie.GetComponent<NavMeshAgent>().enabled = true;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Level") return;
            hasBeenThrown = true;
            _arbie.GetComponent<NavMeshAgent>().enabled = false;
        }

        IEnumerator WaitFor(float waitPeriod)
        {
            yield return new WaitForSeconds(waitPeriod);
        }
    }
}
