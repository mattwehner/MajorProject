using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        void Start () {
            Debug.Log("CameraController Is Alive");
        }
        public void BoundryMovementMaster(string direction)
            {
              BoundryMovement(direction);
            }
        private void BoundryMovement(string direction)
        {
            var playerMovementSpeed = WorldStorage.PlayerMovementSpeed;
            if (direction == "move left")
            {
                transform.Translate(new Vector3(-playerMovementSpeed, 0) * Time.deltaTime);
            }
            if (direction == "move right")
            {
                transform.Translate(new Vector3(playerMovementSpeed, 0) * Time.deltaTime);
            }
        }
    }
}
