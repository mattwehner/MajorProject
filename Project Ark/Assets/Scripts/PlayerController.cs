using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        public static PlayerController playerController;
        void Awake()
        {
            playerController = this;

        }

        void Start () {
            Debug.Log("CameraController Is Alive");
        }
        public void BoundryMovementMaster(string direction)
            {
              BoundryMovement(direction);
            }
        private void BoundryMovement(string direction)
        {
            var playerMovementSpeed = Settings.Player.PlayerMovementSpeed;
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
