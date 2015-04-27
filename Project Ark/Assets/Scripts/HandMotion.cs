using UnityEngine;
using Leap;

namespace Assets.Scripts
{
    public class HandMotion : MonoBehaviour
    {
        private WorldStorage _worldStorage;

        private Frame _frame;
        private Hand _hand;
        private float _minHandHeight;

        void Start ()
        {
            Debug.Log("HandMotion Is Alive");

            _worldStorage = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldStorage>();

            _minHandHeight = PublicReferenceList.MinHandHeight;
        }
	
        void Update () {
            _frame = _worldStorage.Frame;
            _hand = _worldStorage.Hand;

            var interactionBox = _frame.InteractionBox;
            var handPosition = interactionBox.NormalizePoint(_hand.StabilizedPalmPosition);
            
            if (!_hand.IsValid)
            {
                return;
            }

            if (handPosition.y < _minHandHeight)
            {
                _worldStorage.IsPaused = true;
            }
        }
    }
}
