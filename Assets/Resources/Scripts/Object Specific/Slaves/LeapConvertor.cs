using Leap;
using UnityEngine;

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

            var unityPosition = original.ToUnityScaled();
            var worldPosition = _handController.transform.TransformPoint(unityPosition);
            return worldPosition;
        }
    }
}