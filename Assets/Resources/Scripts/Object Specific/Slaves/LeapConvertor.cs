using UnityEngine;
using System.Collections;
using Leap;

public class LeapConvertor
{
    
    public class ToWorld
    {
        private GameObject _handController;
        public Vector3 Point(Vector original)
        {
            if (_handController == null)
            {
                _handController = GameObject.FindWithTag("GameController");
            }

            Vector3 unityPosition = original.ToUnityScaled();
            Vector3 worldPosition = _handController.transform.TransformPoint(unityPosition);
            return worldPosition;
        }
    }
}
