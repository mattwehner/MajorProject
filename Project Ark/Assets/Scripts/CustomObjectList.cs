using UnityEngine;

namespace Assets.Scripts
{
    public class CoordinatesObject
    {
        public CoordinatesObject() { }

        public CoordinatesObject(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public override string ToString()
        {
            return "x:"+ x + ", y:" + y + ",z: " + z;
        }
    }

    public class GameSettings
    {
        public GameSettings() { }

        public GameSettings(
            float playerMovementSpeed,
            float characterSpeed,
            float minHandHeight
            )
        {
            PlayerMovementSpeed = playerMovementSpeed;
            CharacterSpeed = characterSpeed;
            MinHandHeight = minHandHeight;
        }

        public float PlayerMovementSpeed { get; set; }
        public float CharacterSpeed { get; set; }
        public float MinHandHeight { get; set; }
    }
}
