using System;
using System.Collections;
using UnityEngine;

namespace Assets.Resources.Scripts.Object_Specific.Slaves
{
    internal static class Utilities
    {
        public static IEnumerator WaitFor(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

        public static IEnumerator WaitFor(Action beforeAction, float seconds)
        {
            beforeAction();
            yield return new WaitForSeconds(seconds);
        }

        public static IEnumerator WaitFor(float seconds, Action afterAction)
        {
            yield return new WaitForSeconds(seconds);
            afterAction();
        }

        public static IEnumerator WaitFor(Action beforeAction, float seconds, Action afterAction)
        {
            beforeAction();
            yield return new WaitForSeconds(seconds);
            afterAction();
        }

        public static IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
        {
            var rb = objectToMove.GetComponent<Rigidbody>();
            float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
            float t = 0;
            while (t <= 1.0f)
            {
                
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                t += step; // Goes from 0 to 1, incrementing by step each time
                objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
                yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
            }
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            objectToMove.position = b;
        }
    }
}